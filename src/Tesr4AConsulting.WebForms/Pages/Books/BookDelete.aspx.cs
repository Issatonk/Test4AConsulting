using System;
using System.Configuration;
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

        protected async void Page_Load(
            object sender,
            EventArgs e)
        {
            if (!IsPostBack)
            {
                var id = GetBookId();

                if (id == null)
                {
                    Response.Redirect("~/Books.aspx");
                    return;
                }

                var book =
                    await _repository.GetByIdAsync(id.Value);

                if (book == null)
                {
                    Response.Redirect("~/Books.aspx");
                    return;
                }

                TitleLabel.Text = book.Title;
                AuthorLabel.Text = book.Author;
                YearLabel.Text =
                    book.PublicationYear?.ToString() ?? "";

                CancelLink.NavigateUrl =
                    "~/BookDetails.aspx?id=" + book.Id;
            }
        }

        protected async void DeleteButton_Click(
            object sender,
            EventArgs e)
        {
            var id = GetBookId();

            if (id == null)
            {
                Response.Redirect("~/Books.aspx");
                return;
            }

            try
            {
                await _repository.DeleteAsync(id.Value);

                Response.Redirect("~/Books.aspx");
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
    }
}