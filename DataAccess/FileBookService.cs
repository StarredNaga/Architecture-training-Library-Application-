using DataAccess.Interfaces;
using Domain.Entities;

namespace DataAccess;

/// <summary>
/// Class for storage books in file
/// </summary>
public class FileBookService : IBookService
{
    private readonly IFileReader _reader;
    private readonly IFileWriter _writer;
    private readonly IBookFormater _bookFormater;

    public FileBookService(IFileReader reader, IFileWriter writer, IBookFormater bookFormater)
    {
        _reader = reader;
        _writer = writer;
        _bookFormater = bookFormater;

        try
        {
            var books = _bookFormater.DeserializeList(_reader.ReadAllText());

            if (books == null || books.Count == 0)
                _writer.Write(_bookFormater.SerializeList([]));
        }
        catch (Exception e)
        {
            _writer.Write(_bookFormater.SerializeList([]));
        }
    }

    /// <summary>
    ///  Add book to file
    /// </summary>
    /// <param name="book">Book to add</param>
    /// <returns>Added book</returns>
    /// <exception cref="Exception">Deserialize error</exception>
    public async Task<Book> AddBook(Book book)
    {
        var books = new List<Book>();

        books = _bookFormater.DeserializeList(await _reader.ReadAllTextAsync());

        if (books == null)
            throw new Exception();

        books.Add(book);

        await _writer.WriteAsync(_bookFormater.SerializeList(books));

        return book;
    }

    /// <summary>
    ///  Get all books
    /// </summary>
    /// <returns>List of books</returns>
    /// <exception cref="Exception">Deserialize error</exception>
    public async Task<List<Book>> GetBooks()
    {
        var books = new List<Book>();

        books = _bookFormater.DeserializeList(await _reader.ReadAllTextAsync());

        if (books == null)
            throw new Exception();

        return books.ToList();
    }

    /// <summary>
    /// Update book
    /// </summary>
    /// <param name="book">New data of book</param>
    /// <param name="id">Id of book that must be updated</param>
    /// <returns>Book with new data</returns>
    /// <exception cref="Exception">
    /// 1. File doesn't contains book with that id
    /// 2. Deserialize error
    /// </exception>
    public async Task<Book> UpdateBook(Book book, int id)
    {
        var books = _bookFormater.DeserializeList(await _reader.ReadAllTextAsync());

        if (books == null)
            throw new Exception();

        if (books.All(x => x.Id != id))
            throw new Exception();

        var updateBook = books.First(x => x.Id == id);

        if (updateBook == null)
            throw new Exception();

        updateBook.Title = book.Title;
        updateBook.Author = book.Author;
        updateBook.Description = book.Description;
        updateBook.ImageUrl = book.ImageUrl;

        await _writer.WriteAsync(_bookFormater.SerializeList(books));

        return updateBook;
    }

    /// <summary>
    ///  Delete book
    /// </summary>
    /// <param name="id">Id of book that must be deleted</param>
    /// <exception cref="Exception">
    /// 1. Deserialize error
    /// 2. File doesn't contain book with that id
    /// </exception>
    public async Task DeleteBook(int id)
    {
        var books = _bookFormater.DeserializeList(_reader.ReadAllText());

        if (books == null)
            throw new Exception();

        if (books.All(x => x.Id != id))
            throw new Exception();

        var deleteBook = books.First(x => x.Id == id);

        books.Remove(deleteBook);

        if (books.Count == 0)
        {
            await _writer.WriteAsync(_bookFormater.SerializeList([]));

            return;
        }

        await _writer.WriteAsync(_bookFormater.SerializeList(books));
    }
}
