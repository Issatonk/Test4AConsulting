using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Tesr4AConsulting.WebForms.Models;

namespace Tesr4AConsulting.WebForms.Repositories
{
    public class BookRepository
    {
        private readonly Func<IDbConnection> _connectionFactory;

        public BookRepository()
        {
            var conn = ConfigurationManager
                .ConnectionStrings["DefaultConnection"]
                .ConnectionString;

            _connectionFactory = () => new SqlConnection(conn);
        }

        public BookRepository(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("connectionString");

            _connectionFactory = () => new SqlConnection(connectionString);
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
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<Book>(
                    "Book_Select",
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            using (var connection = CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Book>(
                    "Book_GetById",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> InsertAsync(Book book)
        {
            using (var connection = CreateConnection())
            {
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
        }

        public async Task UpdateAsync(Book book)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync(
                    "Book_Update",
                    new
                    {
                        book.Id,
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
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync(
                    "Book_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Book>> SearchByContentsAsync(
            string searchText)
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<Book>(
                    "Book_SearchByContents",
                    new
                    {
                        SearchText = searchText
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<BookContentItem>> GetContentsAsync(
            int id)
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<BookContentItem>(
                    "Book_GetContents",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}