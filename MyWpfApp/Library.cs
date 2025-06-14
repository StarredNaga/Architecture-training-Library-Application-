using DataAccess.Entitites;
using DataAccess.Interfaces;
using Domain.Entities;
using MyWpfApp.Interfaces;

namespace MyWpfApp;

/// <summary>
///  Class for control books storage
/// </summary>
public class Library
{
    private readonly IBookService _bookService;

    public Library(IBookService bookService)
    {
        _bookService = bookService;
    }

    /// <summary>
    ///  Just get all books
    /// </summary>
    /// <returns></returns>
    private async Task<List<Book>> GetBooks() => await _bookService.GetBooks();

    /// <summary>
    ///  Get id by book data
    /// </summary>
    /// <param name="bookDto">Book data to get id</param>
    /// <returns>Id of book with same data</returns>
    private async Task<int> GetIdByDto(BookDto? bookDto)
    {
        var books = await GetBooks();

        return books
                .FirstOrDefault(x =>
                    x.Author == bookDto!.Author
                    && x.ImageUrl == bookDto.ImageUrl
                    && x.Title == bookDto.Title
                    && x.Description == bookDto.Description
                )
                ?.Id ?? -1;
    }

    /// <summary>
    ///  Get books with sort
    /// </summary>
    /// <param name="sort">Sort for books</param>
    /// <returns>List of books DTO</returns>
    public async Task<List<BookDto>> GetBooks(IBookSorter? sort)
    {
        var books = await _bookService.GetBooks();

        return sort?.ApplySort(books)
            ?? books
                .Select(x => new BookDto
                {
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    Title = x.Title,
                    Author = x.Author,
                })
                .ToList();
    }

    /// <summary>
    ///  Get single book with sort
    /// </summary>
    /// <param name="sort">Sort for books</param>
    /// <returns>Single book DTO</returns>
    public async Task<BookDto?> GetBook(IBookSorter? sort)
    {
        var books = await GetBooks(sort);

        return books.FirstOrDefault();
    }

    /// <summary>
    ///  Create book
    /// </summary>
    /// <param name="book">Book to add</param>
    /// <returns>Created book data as DTO</returns>
    public async Task<BookDto> CreateBook(BookDto book)
    {
        var books = await GetBooks();

        var lastId = books.Count > 0 ? books.Select(x => x.Id).Max() : 0;

        var result = await _bookService.AddBook(
            new Book
            {
                Id = lastId + 1,
                Description = book.Description,
                ImageUrl = book.ImageUrl,
                Title = book.Title,
                Author = book.Author,
            }
        );

        return new BookDto
        {
            Description = result.Description,
            ImageUrl = result.ImageUrl,
            Title = result.Title,
            Author = result.Author,
        };
    }

    /// <summary>
    ///  Update book
    /// </summary>
    /// <param name="book">Book to update</param>
    /// <param name="updatedBook">New book data</param>
    /// <returns>Book with updated data</returns>
    /// <exception cref="Exception">File doesn't contain book</exception>
    public async Task<BookDto?> UpdateBook(BookDto? book, BookDto updatedBook)
    {
        var bookId = await GetIdByDto(book);

        if (bookId == -1)
            throw new Exception("Book not found");

        var result = await _bookService.UpdateBook(
            new Book
            {
                Author = updatedBook.Author,
                Description = updatedBook.Description,
                ImageUrl = updatedBook.ImageUrl,
                Title = updatedBook.Title,
            },
            bookId
        );

        return new BookDto
        {
            Author = result.Author,
            Description = result.Description,
            ImageUrl = result.ImageUrl,
            Title = result.Title,
        };
    }

    /// <summary>
    ///  Delete book
    /// </summary>
    /// <param name="book">Book to delete</param>
    /// <exception cref="Exception">File doesn't contain book</exception>
    public async Task DeleteBook(BookDto? book)
    {
        var id = await GetIdByDto(book);

        if (id == -1)
            throw new Exception("Book not found");

        await _bookService.DeleteBook(id);
    }
}
