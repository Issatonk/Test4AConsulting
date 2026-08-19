namespace Test4AConsulting.MVC.Repositories;

using Test4AConsulting.MVC.Models;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync();

    Task<Book?> GetByIdAsync(int id);

    Task<int> InsertAsync(Book book);

    Task UpdateAsync(Book book);

    Task DeleteAsync(int id);

    Task<IEnumerable<Book>> SearchByContentsAsync(string searchText);

    Task<IEnumerable<BookContentItem>> GetContentsAsync(int id);
}
