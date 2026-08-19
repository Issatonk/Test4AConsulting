using Microsoft.AspNetCore.Mvc;
using Test4AConsulting.MVC.Helpers;
using Test4AConsulting.MVC.Models;
using Test4AConsulting.MVC.Repositories;

namespace Test4AConsulting.MVC.Controllers;


public class BooksController : Controller
{
    private readonly IBookRepository _bookRepository;

    public BooksController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IActionResult> Index(string? search)
    {
        IEnumerable<Book> books;

        if (string.IsNullOrWhiteSpace(search))
        {
            books = await _bookRepository.GetAllAsync();
        }
        else
        {
            books = await _bookRepository.SearchByContentsAsync(search);
        }

        ViewBag.Search = search;

        return View(books);
    }

    public async Task<IActionResult> Details(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        if (book == null)
            return NotFound();

        var contents = await _bookRepository.GetContentsAsync(id);

        var model = new BookDetailsViewModel
        {
            Book = book,
            Contents = contents
        };

        return View(model);
    }


    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Book book)
    {
        if (!ModelState.IsValid)
            return View(book);

        try
        {
            book.Contents = XmlContentHelper.ToXml(book.Contents);
        }
        catch
        {
            ModelState.AddModelError(
                nameof(book.Contents),
                "Оглавление содержит некорректную XML/HTML-разметку.");

            return View(book);
        }

        await _bookRepository.InsertAsync(book);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        if (book == null)
            return NotFound();

        book.Contents = XmlContentHelper.FromXml(book.Contents);

        return View(book);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Book book)
    {
        if (!ModelState.IsValid)
            return View(book);

        try
        {
            book.Contents = XmlContentHelper.ToXml(book.Contents);
        }
        catch
        {
            ModelState.AddModelError(
                nameof(book.Contents),
                "Оглавление содержит некорректную XML/HTML-разметку.");

            return View(book);
        }

        await _bookRepository.UpdateAsync(book);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        if (book == null)
            return NotFound();

        return View(book);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _bookRepository.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}
