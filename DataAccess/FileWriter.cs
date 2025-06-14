using DataAccess.Interfaces;
using DataBase;

namespace DataAccess;

/// <summary>
///  Class for write data in file
/// </summary>
public class FileWriter : IFileWriter
{
    private readonly FileConfigs _config;

    public FileWriter(FileConfigs config)
    {
        _config = config;

        if (!File.Exists(_config.Path + _config.Name))
            File.Create(_config.Path + _config.Name).Close();
    }

    /// <summary>
    ///  Rewrite file
    /// </summary>
    /// <param name="text">Data to rewrite</param>
    public void Write(string text)
    {
        using var fs = new FileStream(_config.Path + _config.Name, FileMode.OpenOrCreate);

        using var sw = new StreamWriter(fs);

        sw.Write(text);

        fs.SetLength(text.Length);
    }

    /// <summary>
    ///  Rewrite file async
    /// </summary>
    /// <param name="text">Data to rewrite</param>
    public async Task WriteAsync(string text)
    {
        using var fs = new FileStream(_config.Path + _config.Name, FileMode.OpenOrCreate);

        using var sw = new StreamWriter(fs);

        await sw.WriteAsync(text);

        fs.SetLength(text.Length);
    }
}
