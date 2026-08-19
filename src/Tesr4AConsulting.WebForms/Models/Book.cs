namespace Tesr4AConsulting.WebForms.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public int? PublicationYear { get; set; }

        public string Publisher { get; set; }

        public string Isbn { get; set; }

        public string Description { get; set; }

        public string Contents { get; set; }

        public string FirstContentItem { get; set; }
    }
}