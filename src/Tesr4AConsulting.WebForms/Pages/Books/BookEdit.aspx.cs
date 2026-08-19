using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;
using System.Xml.Linq;
using System.Configuration;
using Tesr4AConsulting.WebForms.Models;
using Tesr4AConsulting.WebForms.Repositories;



namespace Tesr4AConsulting.WebForms.Pages.Books
{
    public partial class BookEdit : System.Web.UI.Page
    {
        private readonly BookRepository _repository =
            new BookRepository(
                ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString);

        protected async void Page_Load(
            object sender,
            EventArgs e)
        {
            if (!IsPostBack)
            {
                await LoadBookAsync();
            }
        }

        private int? GetBookId()
        {
            int id;

            if (int.TryParse(Request.QueryString["id"], out id))
                return id;

            return null;
        }

        private async System.Threading.Tasks.Task LoadBookAsync()
        {
            var id = GetBookId();

            if (id == null)
            {
                Response.Redirect("~/Books.aspx");
                return;
            }

            var book = await _repository.GetByIdAsync(id.Value);

            if (book == null)
            {
                Response.Redirect("~/Books.aspx");
                return;
            }

            TitleTextBox.Text = book.Title;
            AuthorTextBox.Text = book.Author;
            YearTextBox.Text =
                book.PublicationYear?.ToString() ?? "";

            PublisherTextBox.Text = book.Publisher;
            IsbnTextBox.Text = book.Isbn;
            DescriptionTextBox.Text = book.Description;

            ContentsTextBox.Text =
                ExtractContents(book.Contents);

            CancelLink.NavigateUrl =
                "~/BookDetails.aspx?id=" + book.Id;
        }

        protected async void SaveButton_Click(
            object sender,
            EventArgs e)
        {
            RegisterAsyncTask(
                new PageAsyncTask(SaveBookAsync));
        }
        private async Task SaveBookAsync()
        {
            if (!Page.IsValid)
                return;

            var id = GetBookId();

            if (id == null)
            {
                Response.Redirect("~/Books.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            int? publicationYear = null;

            if (!string.IsNullOrWhiteSpace(YearTextBox.Text))
            {
                int year;

                if (!int.TryParse(YearTextBox.Text, out year))
                {
                    ShowError("Год издания указан неверно.");
                    return;
                }

                publicationYear = year;
            }

            string contents = null;

            if (!string.IsNullOrWhiteSpace(ContentsTextBox.Text))
            {
                try
                {
                    contents =
                        "<contents>" +
                        ContentsTextBox.Text +
                        "</contents>";

                    XDocument.Parse(contents);
                }
                catch
                {
                    ShowError(
                        "Оглавление содержит некорректную XML/HTML-разметку.");

                    return;
                }
            }

            var book = new Book
            {
                Id = id.Value,
                Title = TitleTextBox.Text.Trim(),
                Author = AuthorTextBox.Text.Trim(),
                PublicationYear = publicationYear,
                Publisher = PublisherTextBox.Text.Trim(),
                Isbn = IsbnTextBox.Text.Trim(),
                Description = DescriptionTextBox.Text.Trim(),
                Contents = contents
            };

            await _repository.UpdateAsync(book);

            Response.Redirect(
                "~/BookDetails.aspx?id=" + id.Value,
                false);

            Context.ApplicationInstance.CompleteRequest();
        }
        private string ExtractContents(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
                return string.Empty;

            var document = XDocument.Parse(xml);

            if (document.Root == null)
                return string.Empty;

            return string.Concat(
                document.Root.Nodes()
                    .Select(x => x.ToString()));
        }

        private void ShowError(string message)
        {
            ErrorLabel.Text = message;
            ErrorLabel.Visible = true;
        }
    }
}