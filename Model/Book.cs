namespace LibraryManagementSystem.Model;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public bool IsAvailable { get; set; } = true;

    public Book(int id, string title, string author)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(author);

        Id = id;
        Title = title.Trim();
        Author = author.Trim();
    }
}
