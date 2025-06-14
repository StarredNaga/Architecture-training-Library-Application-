using DataAccess.Interfaces;
using DataBase;

namespace DataAccess;

/// <summary>
///  Class for read data from file
/// </summary>
public class FileReader : IFileReader
{
    private readonly FileConfigs _config;

    public FileReader(FileConfigs config)
    {
        _config = config;

        if (!File.Exists(_config.Path + _config.Name))
            File.Create(_config.Path + _config.Name).Close();
    }

    /// <summary>
    ///  Read text from file
    /// </summary>
    /// <returns>File data as string</returns>
    public string ReadAllText()
    {
        using var fs = new FileStream(_config.Path + _config.Name, FileMode.Open);

        using var sr = new StreamReader(fs);

        return sr.ReadToEnd();
    }

    /// <summary>
    ///  Read text from file async
    /// </summary>
    /// <returns>File data as string</returns>
    public async Task<string> ReadAllTextAsync()
    {
        using var fs = new FileStream(_config.Path + _config.Name, FileMode.Open);

        using var sr = new StreamReader(fs);

        return await sr.ReadToEndAsync();
    }
}
