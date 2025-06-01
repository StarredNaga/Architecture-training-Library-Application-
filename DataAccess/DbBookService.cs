using DataAccess.Interfaces;
using DataBase;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class DbBookService : IBookService
{
    private readonly BookDbContext _dbContext;

    public DbBookService(BookDbContext dbContext)
    {
        _dbContext = dbContext;

        _dbContext.Initialize();
    }

    public async Task<Book> AddBook(Book book)
    {
        _dbContext.Books.Add(book);

        await _dbContext.SaveChangesAsync();

        return book;
    }

    public async Task<List<Book>> GetBooks()
    {
        return await _dbContext.Books.AsNoTracking().ToListAsync();
    }

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

    public async Task DeleteBook(int id)
    {
        if (!_dbContext.Books.Any(b => b.Id == id))
            throw new Exception("Book not found");

        var book = _dbContext.Books.Single(b => b.Id == id);

        _dbContext.Books.Remove(book);

        await _dbContext.SaveChangesAsync();
    }
}
