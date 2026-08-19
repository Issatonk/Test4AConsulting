using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using Test4AConsulting.MVC.Models;

namespace Test4AConsulting.MVC.Repositories;

public class BookRepository : IBookRepository
{
    private readonly Func<IDbConnection> _connectionFactory;

    public BookRepository(IConfiguration configuration)
        : this(() => new SqlConnection(
            configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Connection string DefaultConnection not found.")))
    {
    }

    public BookRepository(Func<IDbConnection> connectionFactory)
    {
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
    }

    private IDbConnection CreateConnection()
    {
        return _connectionFactory();
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        using var connection = CreateConnection();

        return await connection.QueryAsync<Book>(
            "Book_Select",
            commandType: CommandType.StoredProcedure);
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        using var connection = CreateConnection();

        return await connection.QuerySingleOrDefaultAsync<Book>(
            "Book_GetById",
            new { Id = id },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<int> InsertAsync(Book book)
    {
        using var connection = CreateConnection();

        return await connection.QuerySingleAsync<int>(
            "Book_Insert",
            new
            {
                book.Title,
                book.Author,
                book.PublicationYear,
                book.Publisher,
                book.Isbn,
                book.Description,
                book.Contents
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task UpdateAsync(Book book)
    {
        using var connection = CreateConnection();

        await connection.ExecuteAsync(
            "Book_Update",
            book,
            commandType: CommandType.StoredProcedure);
    }

    public async Task DeleteAsync(int id)
    {
        using var connection = CreateConnection();

        await connection.ExecuteAsync(
            "Book_Delete",
            new { Id = id },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Book>> SearchByContentsAsync(string searchText)
    {
        using var connection = CreateConnection();

        return await connection.QueryAsync<Book>(
            "Book_SearchByContents",
            new
            {
                SearchText = searchText
            },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<BookContentItem>> GetContentsAsync(int id)
    {
        using var connection = CreateConnection();

        return await connection.QueryAsync<BookContentItem>(
            "Book_GetContents",
            new
            {
                Id = id
            },
            commandType: CommandType.StoredProcedure);
    }
}