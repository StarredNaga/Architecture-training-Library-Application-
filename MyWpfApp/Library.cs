using DataAccess.Entitites;
using DataAccess.Interfaces;
using Domain.Entities;
using MyWpfApp.Interfaces;

namespace MyWpfApp;

public class Library
{
    private IBookService _bookService;

    public Library(IBookService bookService)
    {
        _bookService = bookService;
    }

    private List<Book> GetBooks() => _bookService.GetBooks();

    private int GetIdByDto(BookDto? bookDto)
    {
        var books = GetBooks();

        return books
                .FirstOrDefault(x =>
                    x.Author == bookDto!.Author
                    && x.ImageUrl == bookDto.ImageUrl
                    && x.Title == bookDto.Title
                    && x.Description == bookDto.Description
                )
                ?.Id ?? -1;
    }

    public List<BookDto> GetBooks(IBookSorter? sort)
    {
        var books = _bookService.GetBooks();

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

    public BookDto? GetBook(IBookSorter? sort)
    {
        return GetBooks(sort).FirstOrDefault();
    }

    public BookDto CreateBook(BookDto book)
    {
        var books = GetBooks();

        var lastId = books.Count > 0 ? GetBooks().Select(x => x.Id).Max() : 0;

        var result = _bookService.AddBook(
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

    public BookDto? UpdateBook(BookDto? book, BookDto updatedBook)
    {
        var bookId = GetIdByDto(book);

        if (bookId == -1)
            throw new Exception("Book not found");

        var result = _bookService.UpdateBook(
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

    public void DeleteBook(BookDto? book)
    {
        var id = GetIdByDto(book);

        if (id == -1)
            throw new Exception("Book not found");

        _bookService.DeleteBook(id);
    }
}
