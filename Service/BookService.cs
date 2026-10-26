namespace LibraryManagementSystem.Service;

using LibraryManagementSystem.Model;
using LibraryManagementSystem.Repository;

public class BookService(IBookRepository repository) : IBookService
{
    private readonly IBookRepository _repository = repository;
    private int _nextBookId = 1;

    public Book AddBook(string title, string author)
    {
        var book = new Book(_nextBookId++, title, author);
        _repository.Add(book);
        return book;
    }

    public IReadOnlyList<Book> GetAllBooks() => _repository.GetAll();

    public IReadOnlyList<Book> GetAvailableBooks() => _repository.GetAvailable();

    public Book? GetBookById(int id) => _repository.GetById(id);

    public IReadOnlyList<Book> SearchAvailableBooks(string term) => _repository.SearchAvailable(term);

    public IReadOnlyList<Book> SearchBooks(string term) => _repository.Search(term);
}
