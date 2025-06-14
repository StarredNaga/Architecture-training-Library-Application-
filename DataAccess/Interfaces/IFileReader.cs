namespace DataAccess.Interfaces;

public interface IFileReader
{
    public string ReadAllText();

    public Task<string> ReadAllTextAsync();
}
