using DataBase.Interfaces;

namespace DataBase;

public class FileReader : IFileReader
{
    private FileConfigs _config;

    public FileReader(FileConfigs config)
    {
        _config = config;

        if (!File.Exists(_config.Path + _config.Name))
            File.Create(_config.Path + _config.Name).Close();
    }

    public string ReadAllText()
    {
        using var fs = new FileStream(_config.Path + _config.Name, FileMode.Open);

        using var sr = new StreamReader(fs);

        return sr.ReadToEnd();
    }
}
