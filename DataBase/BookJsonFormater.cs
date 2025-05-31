using DataBase.Interfaces;
using Domain.Entities;
using Newtonsoft.Json;

namespace DataBase;

public class BookJsonFormater : IBookFormater
{
    public string Serialize(Book book)
    {
        return JsonConvert.SerializeObject(book);
    }

    public string SerializeList(List<Book> books)
    {
        return JsonConvert.SerializeObject(books, Formatting.Indented);
    }

    public Book? Deserialize(string data)
    {
        return JsonConvert.DeserializeObject<Book>(data);
    }

    public List<Book>? DeserializeList(string data)
    {
        return JsonConvert.DeserializeObject<List<Book>>(data);
    }
}
