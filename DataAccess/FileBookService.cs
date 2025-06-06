﻿using DataAccess.Interfaces;
using DataBase.Interfaces;
using Domain.Entities;

namespace DataAccess;

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

    public async Task<Book> AddBook(Book book)
    {
        var books = new List<Book>();

        await Task.Run(() =>
        {
            books = _bookFormater.DeserializeList(_reader.ReadAllText());
        });

        if (books == null)
            throw new Exception();

        books.Add(book);

        await Task.Run(() =>
        {
            _writer.Write(_bookFormater.SerializeList(books));
        });

        return book;
    }

    public async Task<List<Book>> GetBooks()
    {
        var books = new List<Book>();

        await Task.Run(() =>
        {
            books = _bookFormater.DeserializeList(_reader.ReadAllText());
        });

        if (books == null)
            throw new Exception();

        return books.ToList();
    }

    public async Task<Book> UpdateBook(Book book, int id)
    {
        var books = _bookFormater.DeserializeList(_reader.ReadAllText());

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

        await Task.Run(() =>
        {
            _writer.Write(_bookFormater.SerializeList(books));
        });

        return updateBook;
    }

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
            _writer.Write(_bookFormater.SerializeList([]));

            return;
        }

        await Task.Run(() =>
        {
            _writer.Write(_bookFormater.SerializeList(books));
        });
    }
}
