using DataAccess;
using DataAccess.Interfaces;
using DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Configurations;

/// <summary>
/// Class for implement configurations
/// </summary>
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

    /// <summary>
    ///  Include Entity framework core and storage books in postreSQL database
    /// </summary>
    /// <returns></returns>
    public IServiceProvider UseDataBase()
    {
        _services
            .AddSingleton(new DbConfigs { ConnectionString = "db configs here" })
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

    /// <summary>
    ///  Include storage in file
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    ///  Builds ServiceProvider
    /// </summary>
    /// <returns></returns>
    public IServiceProvider ConfigureServices()
    {
        _provider = _services.BuildServiceProvider();

        return _provider;
    }
}
