using System;
using System.Threading.Tasks;
using System.Configuration;
using Tesr4AConsulting.WebForms.Repositories;

namespace Tesr4AConsulting.WebForms.Pages.Books
{
    public partial class Books : System.Web.UI.Page
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
                await LoadBooksAsync();
            }
        }

        private async Task LoadBooksAsync()
        {
            var books = await _repository.GetAllAsync();

            BooksGrid.DataSource = books;
            BooksGrid.DataBind();
        }

        protected async void SearchButton_Click(
            object sender,
            EventArgs e)
        {
            var search = SearchTextBox.Text;

            if (string.IsNullOrWhiteSpace(search))
            {
                await LoadBooksAsync();
                return;
            }

            var books =
                await _repository.SearchByContentsAsync(search);

            BooksGrid.DataSource = books;
            BooksGrid.DataBind();
        }

        protected async void ResetButton_Click(
            object sender,
            EventArgs e)
        {
            SearchTextBox.Text = string.Empty;

            await LoadBooksAsync();
        }
    }
}