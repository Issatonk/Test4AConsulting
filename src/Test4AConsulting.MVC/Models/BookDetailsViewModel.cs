namespace Test4AConsulting.MVC.Models;

public class BookDetailsViewModel
{
    public Book Book { get; set; } = null!;

    public IEnumerable<BookContentItem> Contents { get; set; }
        = Enumerable.Empty<BookContentItem>();
}