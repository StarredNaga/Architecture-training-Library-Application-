namespace DataAccess.Interfaces;

public interface IFileWriter
{
    public void Write(string text);

    public Task WriteAsync(string text);
}
