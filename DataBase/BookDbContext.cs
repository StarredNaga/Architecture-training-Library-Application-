using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataBase;

public class BookDbContext : DbContext
{
    public DbSet<Book> Books { get; set; } = null!;

    public BookDbContext(DbContextOptions<BookDbContext> options)
        : base(options) { }

    public void Initialize()
    {
        Database.EnsureCreated();
    }
}
