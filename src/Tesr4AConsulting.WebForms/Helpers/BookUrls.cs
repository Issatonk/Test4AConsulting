namespace Tesr4AConsulting.WebForms.Helpers
{
    public static class BookUrls
    {
        private const string BooksPath = "~/Pages/Books/";

        public static string Books()
            => BooksPath + "Books.aspx";

        public static string BookCreate()
            => BooksPath + "BookCreate.aspx";

        public static string BookDetails(int id)
            => $"{BooksPath}BookDetails.aspx?id={id}";

        public static string BookEdit(int id)
            => $"{BooksPath}BookEdit.aspx?id={id}";

        public static string BookDelete(int id)
            => $"{BooksPath}BookDelete.aspx?id={id}";
    }
}
