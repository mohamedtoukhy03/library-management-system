namespace LibraryManagementSystem.Repository;

using LibraryManagementSystem.Model;

public class InMemoryBookRepository : IBookRepository
{
    private readonly List<Book> _books = [];

    public void Add(Book book)
    {
        _books.Add(book);
    }

    public Book? GetById(int id) => _books.FirstOrDefault(book => book.Id == id);

    public IReadOnlyList<Book> GetAll() => _books.AsReadOnly();

    public IReadOnlyList<Book> GetAvailable() {
        return _books
            .Where(book => book.IsAvailable)
            .ToList().AsReadOnly();
    }

    public IReadOnlyList<Book> Search(string term)
    {
        if (string.IsNullOrWhiteSpace(term))
            return GetAll();

        return _books
            .Where(book => book.Title.ToLower().Contains(term.ToLower()) ||
                        book.Author.ToLower().Contains(term.ToLower()))
            .ToList().AsReadOnly();
    }

    public IReadOnlyList<Book> SearchAvailable(string term) =>
        Search(term).Where(book => book.IsAvailable).ToList().AsReadOnly();
}

