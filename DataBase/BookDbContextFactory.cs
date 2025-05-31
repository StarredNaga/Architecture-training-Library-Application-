using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataBase;

public class BookDbContextFactory : IDesignTimeDbContextFactory<BookDbContext>
{
    public BookDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BookDbContext>();
        optionsBuilder.UseNpgsql(
            "User ID=postgres;Password=57912021ASK;Host=localhost;Port=5432;Database=TrainingDb;"
        );
        return new BookDbContext(optionsBuilder.Options);
    }
}
