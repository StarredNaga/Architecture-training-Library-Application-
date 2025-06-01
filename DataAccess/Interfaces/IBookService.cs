using Domain.Entities;

namespace DataAccess.Interfaces;

public interface IBookService
{
    public Task<Book> AddBook(Book book);

    public Task<List<Book>> GetBooks();

    public Task<Book> UpdateBook(Book book, int id);

    public Task DeleteBook(int id);
}
