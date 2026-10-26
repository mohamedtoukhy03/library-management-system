namespace LibraryManagementSystem.Model;

public class Book
{
    public int Id { get; init; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public bool IsAvailable { get; private set; } = true;

    public Book(int id, string title, string author)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(author);

        Id = id;
        Title = title.Trim();
        Author = author.Trim();
    }

    public void MarkAsBorrowed()
    {
        if (!IsAvailable)
            throw new InvalidOperationException($"'{Title}' is already borrowed.");

        IsAvailable = false;
    }

    public void MarkAsReturned()
    {
        IsAvailable = true;
    }
}
