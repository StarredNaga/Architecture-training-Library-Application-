using DataAccess;
using DataAccess.Interfaces;
using DataBase;
using DataBase.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Configurations;

public class Configurations
{
    private IServiceCollection _services;

    private IServiceProvider _provider;

    public Configurations()
    {
        _services = new ServiceCollection();
        _provider = _services.BuildServiceProvider();
    }

    public IServiceCollection Services => _services;

    public IServiceProvider Provider => _provider;

    public IServiceProvider UseDataBase()
    {
        _services
            .AddSingleton(
                new DbConfigs
                {
                    ConnectionString =
                        "User ID=postgres;Password=57912021ASK;Host=localhost;Port=5432;Database=TrainingDb;",
                }
            )
            .AddDbContext<BookDbContext>(
                (serviceProvider, options) =>
                {
                    var dbConfigs = serviceProvider.GetRequiredService<DbConfigs>();
                    options.UseNpgsql(dbConfigs.ConnectionString);
                }
            )
            .AddSingleton<IBookService, DbBookService>();

        _provider = _services.BuildServiceProvider();

        return _provider;
    }

    public IServiceProvider UseFile()
    {
        _services
            .AddSingleton(
                new FileConfigs { Name = "\\Books.json", Path = Environment.CurrentDirectory }
            )
            .AddSingleton<IFileReader, FileReader>()
            .AddSingleton<IFileWriter, FileWriter>()
            .AddSingleton<IBookFormater, BookJsonFormater>()
            .AddSingleton<IBookService, FileBookService>();

        _provider = _services.BuildServiceProvider();

        return _provider;
    }

    public IServiceProvider ConfigureServices()
    {
        _provider = _services.BuildServiceProvider();

        return _provider;
    }
}
