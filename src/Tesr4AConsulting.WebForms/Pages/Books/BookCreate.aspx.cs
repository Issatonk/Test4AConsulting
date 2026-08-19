using System;
using System.Threading.Tasks;
using System.Web.UI;
using System.Xml.Linq;
using System.Configuration;
using Tesr4AConsulting.WebForms.Models;
using Tesr4AConsulting.WebForms.Repositories;

namespace Tesr4AConsulting.WebForms.Pages.Books
{
    public partial class BookCreate : System.Web.UI.Page
    {
        private readonly BookRepository _repository =
            new BookRepository(
                ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString);

        protected void Page_Load(
            object sender,
            EventArgs e)
        {
        }

        protected void SaveButton_Click(
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
                Title = TitleTextBox.Text.Trim(),
                Author = AuthorTextBox.Text.Trim(),
                PublicationYear = publicationYear,
                Publisher = PublisherTextBox.Text.Trim(),
                Isbn = IsbnTextBox.Text.Trim(),
                Description = DescriptionTextBox.Text.Trim(),
                Contents = contents
            };

            var id = await _repository.InsertAsync(book);

            Response.Redirect(
                "~/BookDetails.aspx?id=" + id,
                false);

            Context.ApplicationInstance.CompleteRequest();
        }

        private void ShowError(string message)
        {
            ErrorLabel.Text = message;
            ErrorLabel.Visible = true;
        }
    }
}