using DataAccess.Interfaces;
using DataBase;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

/// <summary>
///  Class for storage books in database
/// </summary>
public class DbBookService : IBookService
{
    private readonly BookDbContext _dbContext;

    public DbBookService(BookDbContext dbContext)
    {
        _dbContext = dbContext;

        _dbContext.Initialize();
    }

    /// <summary>
    ///  Add book to database
    /// </summary>
    /// <param name="book"></param>
    /// <returns>Added book</returns>
    public async Task<Book> AddBook(Book book)
    {
        _dbContext.Books.Add(book);

        await _dbContext.SaveChangesAsync();

        return book;
    }

    /// <summary>
    ///  Get list of books
    /// </summary>
    /// <returns>List of books</returns>
    public async Task<List<Book>> GetBooks()
    {
        return await _dbContext.Books.AsNoTracking().ToListAsync();
    }

    /// <summary>
    ///  Update book
    /// </summary>
    /// <param name="book">New data for book</param>
    /// <param name="id">Id of book that need to update</param>
    /// <returns>Updated book</returns>
    /// <exception cref="Exception">If database doesn't contains book with that id</exception>
    public async Task<Book> UpdateBook(Book book, int id)
    {
        if (!_dbContext.Books.Any(b => b.Id == id))
            throw new Exception("Book not found");

        var updateBook = _dbContext.Books.Single(b => b.Id == id);

        updateBook.Title = book.Title;
        updateBook.Author = book.Author;
        updateBook.Description = book.Description;
        updateBook.ImageUrl = book.ImageUrl;

        await _dbContext.SaveChangesAsync();

        return updateBook;
    }

    /// <summary>
    ///  Delete book
    /// </summary>
    /// <param name="id">Id of book that u want delete</param>
    /// <exception cref="Exception">If database doesn't contains book with that id</exception>
    public async Task DeleteBook(int id)
    {
        if (!_dbContext.Books.Any(b => b.Id == id))
            throw new Exception("Book not found");

        var book = _dbContext.Books.Single(b => b.Id == id);

        _dbContext.Books.Remove(book);

        await _dbContext.SaveChangesAsync();
    }
}
