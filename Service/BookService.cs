namespace LibraryManagementSystem.Service;

using LibraryManagementSystem.Model;
using LibraryManagementSystem.Repository;
using LibraryManagementSystem.Validation;

public class BookService(IBookRepository repository, IBookValidator validator) : IBookService
{
    private readonly IBookRepository _repository = repository;
    private readonly IBookValidator _validator = validator;
    private int _nextBookId = 1;

    public Book AddBook(string title, string author)
    {
        var validation = _validator.Validate(title, author);
        if (!validation.IsValid)
            throw new ArgumentException(validation.ErrorMessage);

        var book = new Book(_nextBookId++, title.Trim(), author.Trim());
        _repository.Add(book);
        return book;
    }

    public IReadOnlyList<Book> GetAllBooks() => _repository.GetAll();

    public IReadOnlyList<Book> GetAvailableBooks() => _repository.GetAvailable();

    public Book? GetBookById(int id) => _repository.GetById(id);

    public IReadOnlyList<Book> SearchAvailableBooks(string term) => _repository.SearchAvailable(term);

    public IReadOnlyList<Book> SearchBooks(string term) => _repository.Search(term);
}
