using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataBase;

public class BookDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    public BookDbContext(DbContextOptions<BookDbContext> options)
        : base(options) { }

    public void Initialize()
    {
        Database.EnsureCreated();
    }
}
