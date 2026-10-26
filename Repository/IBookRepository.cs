namespace LibraryManagementSystem.Repository;

using LibraryManagementSystem.Model;

public interface IBookRepository
{
    void Add(Book book);
    Book? GetById(int id);
    IReadOnlyList<Book> GetAll();
    IReadOnlyList<Book> GetAvailable();
    IReadOnlyList<Book> Search(string term);
    IReadOnlyList<Book> SearchAvailable(string term);
}

