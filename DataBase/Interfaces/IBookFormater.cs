using Domain.Entities;

namespace DataBase.Interfaces;

public interface IBookFormater
{
    public string Serialize(Book book);

    public string SerializeList(List<Book> books);

    public Book? Deserialize(string data);

    public List<Book>? DeserializeList(string data);
}
