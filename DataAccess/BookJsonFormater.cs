using DataAccess.Interfaces;
using Domain.Entities;
using Newtonsoft.Json;

namespace DataAccess;

/// <summary>
/// Serialize and Deserialize Book to Json
/// </summary>
public class BookJsonFormater : IBookFormater
{
    /// <summary>
    ///  Serialize single book to Json
    /// </summary>
    /// <param name="book"></param>
    /// <returns></returns>
    public string Serialize(Book book)
    {
        return JsonConvert.SerializeObject(book);
    }

    /// <summary>
    ///  Serialize list of books to Json
    /// </summary>
    /// <param name="books"></param>
    /// <returns></returns>
    public string SerializeList(List<Book> books)
    {
        return JsonConvert.SerializeObject(books, Formatting.Indented);
    }

    /// <summary>
    ///  Deserialize json string to book
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public Book? Deserialize(string data)
    {
        return JsonConvert.DeserializeObject<Book>(data);
    }

    /// <summary>
    ///  Deserialize json string to list of books
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public List<Book>? DeserializeList(string data)
    {
        return JsonConvert.DeserializeObject<List<Book>>(data);
    }
}
