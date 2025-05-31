using DataAccess.Entitites;
using Domain.Entities;

namespace DataAccess.Interfaces;

public interface IBookService
{
    public Book AddBook(Book book);

    public List<Book> GetBooks();

    public Book UpdateBook(Book book, int id);

    public void DeleteBook(int id);
}
