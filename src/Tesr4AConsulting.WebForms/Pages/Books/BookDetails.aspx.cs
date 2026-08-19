using System;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Tesr4AConsulting.WebForms.Repositories;

namespace Tesr4AConsulting.WebForms.Pages.Books
{
    public partial class BookDetails : System.Web.UI.Page
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

        private async Task LoadBookAsync()
        {
            int id;

            if (!int.TryParse(Request.QueryString["id"], out id))
            {
                Response.Redirect("~/Books.aspx");
                return;
            }

            var book = await _repository.GetByIdAsync(id);

            if (book == null)
            {
                Response.Redirect("~/Books.aspx");
                return;
            }

            TitleLabel.Text = book.Title;
            AuthorLabel.Text = book.Author;
            YearLabel.Text = book.PublicationYear?.ToString() ?? "";
            PublisherLabel.Text = book.Publisher;
            IsbnLabel.Text = book.Isbn;
            DescriptionLabel.Text = book.Description;

            EditLink.NavigateUrl =
                "~/BookEdit.aspx?id=" + book.Id;

            DeleteLink.NavigateUrl =
                "~/BookDelete.aspx?id=" + book.Id;

            var contents =
                (await _repository.GetContentsAsync(id))
                .ToList();

            if (contents.Count == 0)
            {
                ContentsList.Visible = false;
                NoContentsLabel.Visible = true;
                return;
            }

            foreach (var item in contents)
            {
                ContentsList.Items.Add(item.ContentItem);
            }
        }
    }
}