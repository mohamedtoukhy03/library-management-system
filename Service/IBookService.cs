namespace LibraryManagementSystem.Service;

using LibraryManagementSystem.Model;

public interface IBookService
{
    Book AddBook(string title, string author);
    IReadOnlyList<Book> GetAllBooks();
    IReadOnlyList<Book> GetAvailableBooks();
    Book? GetBookById(int id);
    IReadOnlyList<Book> SearchBooks(string term);
    IReadOnlyList<Book> SearchAvailableBooks(string term);
}
