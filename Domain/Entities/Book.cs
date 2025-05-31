namespace Domain.Entities;

public class Book
{
    public int Id { get; init; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;
}
