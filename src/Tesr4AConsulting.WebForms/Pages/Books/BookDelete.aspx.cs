using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.UI;
using Tesr4AConsulting.WebForms.Helpers;
using Tesr4AConsulting.WebForms.Repositories;

namespace Tesr4AConsulting.WebForms.Pages.Books
{
    public partial class BookDelete : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                RegisterAsyncTask(
                    new PageAsyncTask(LoadBookAsync));
            }
        }

        private async Task LoadBookAsync()
        {
            var id = GetBookId();

            if (id == null)
            {
                RedirectToBooks();
                return;
            }

            var book =
                await _repository.GetByIdAsync(id.Value);

            if (book == null)
            {
                RedirectToBooks();
                return;
            }

            TitleLabel.Text = book.Title;
            AuthorLabel.Text = book.Author;
            YearLabel.Text =
                book.PublicationYear?.ToString() ?? "";

            CancelLink.NavigateUrl =
                BookUrls.BookDetails(book.Id);
        }

        protected void DeleteButton_Click(
            object sender,
            EventArgs e)
        {
            RegisterAsyncTask(
                new PageAsyncTask(DeleteBookAsync));
        }

        private async Task DeleteBookAsync()
        {
            var id = GetBookId();

            if (id == null)
            {
                RedirectToBooks();
                return;
            }

            try
            {
                await _repository.DeleteAsync(id.Value);

                RedirectToBooks();
            }
            catch (Exception ex)
            {
                ErrorLabel.Text =
                    "Ошибка при удалении: " + ex.Message;

                ErrorLabel.Visible = true;
            }
        }

        private int? GetBookId()
        {
            int id;

            if (int.TryParse(
                Request.QueryString["id"],
                out id))
            {
                return id;
            }

            return null;
        }

        private void RedirectToBooks()
        {
            Response.Redirect(
                BookUrls.Books(),
                false);

            Context.ApplicationInstance.CompleteRequest();
        }
    }
}