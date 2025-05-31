using DataBase.Interfaces;

namespace DataBase;

public class FileWriter : IFileWriter
{
    private FileConfigs _config;

    public FileWriter(FileConfigs config)
    {
        _config = config;

        if (!File.Exists(_config.Path + _config.Name))
            File.Create(_config.Path + _config.Name).Close();
    }

    public void Write(string text)
    {
        using var fs = new FileStream(_config.Path + _config.Name, FileMode.OpenOrCreate);

        using var sw = new StreamWriter(fs);

        sw.Write(text);

        fs.SetLength(text.Length);
    }
}
