using DataAccess.Interfaces;
using DataBase;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class DbBookService : IBookService
{
    private BookDbContext _dbContext;

    public DbBookService(BookDbContext dbContext)
    {
        _dbContext = dbContext;

        _dbContext.Initialize();
    }

    public Book AddBook(Book book)
    {
        _dbContext.Books.Add(book);

        _dbContext.SaveChanges();

        return book;
    }

    public List<Book> GetBooks()
    {
        return _dbContext.Books.AsNoTracking().ToList();
    }

    public Book UpdateBook(Book book, int id)
    {
        if (!_dbContext.Books.Any(b => b.Id == id))
            throw new Exception("Book not found");

        var updateBook = _dbContext.Books.Single(b => b.Id == id);

        updateBook.Title = book.Title;
        updateBook.Author = book.Author;
        updateBook.Description = book.Description;
        updateBook.ImageUrl = book.ImageUrl;

        _dbContext.SaveChanges();

        return updateBook;
    }

    public void DeleteBook(int id)
    {
        if (!_dbContext.Books.Any(b => b.Id == id))
            throw new Exception("Book not found");

        var book = _dbContext.Books.Single(b => b.Id == id);

        _dbContext.Books.Remove(book);

        _dbContext.SaveChanges();
    }
}
