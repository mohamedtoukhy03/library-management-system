namespace LibraryManagementSystem.UI;

using LibraryManagementSystem.Model;
using LibraryManagementSystem.Service;

public class ConsoleMenu(
    IBookService bookService,
    IMemberService memberService,
    ILoanService loanService)
{
    private readonly IBookService _bookService = bookService;
    private readonly IMemberService _memberService = memberService;
    private readonly ILoanService _loanService = loanService;

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\n--- Library Management System ---");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. List Books");
            Console.WriteLine("3. Search Books");
            Console.WriteLine("4. Register Member");
            Console.WriteLine("5. List Members");
            Console.WriteLine("6. Borrow Book");
            Console.WriteLine("7. Return Book");
            Console.WriteLine("8. Member Loans");
            Console.WriteLine("9. Overdue Loans");
            Console.WriteLine("0. Exit");

            Console.Write("Choice: ");
            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input.");
                continue;
            }

            if (choice == 0)
            {
                Console.WriteLine("Goodbye.");
                break;
            }

            Action action = choice switch
            {
                1 => AddBook,
                2 => ListAllBooks,
                3 => SearchBooks,
                4 => RegisterMember,
                5 => ListMembers,
                6 => BorrowBook,
                7 => ReturnBook,
                8 => ListActiveLoansForMember,
                9 => ListOverdueLoans,
                _ => InvalidOption
            };
            action();
        }
    }

    private void AddBook()
    {
        Console.WriteLine("--- Add Book ---");
        Console.Write("Title: ");
        string title = Console.ReadLine() ?? "";
        Console.Write("Author: ");
        string author = Console.ReadLine() ?? "";

        try
        {
            var message = _bookService.AddBook(title, author);
            Console.WriteLine(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void ListAllBooks()
    {
        Console.WriteLine("--- Books ---");
        PrintBooks(_bookService.GetAllBooks());
    }

    private void SearchBooks()
    {
        Console.Write("Enter search keyword (title or author): ");
        string term = Console.ReadLine() ?? "";
        var results = _bookService.SearchBooks(term);
        Console.WriteLine($"Found {results.Count} matching book(s):");
        PrintBooks(results);
    }

    private void PrintBooks(IReadOnlyList<Book> books)
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No books found.");
            return;
        }

        foreach (var book in books)
        {
            string status = book.IsAvailable ? "Available" : "NotAvailable";
            Console.WriteLine($"[{book.Id}] {book.Title} by {book.Author} - {status}");
        }
    }

    private void RegisterMember()
    {
        Console.WriteLine("--- Register Member ---");
        Console.Write("Name: ");
        string name = Console.ReadLine() ?? "";

        try
        {
            var message = _memberService.RegisterMember(name, MemberType.Regular);
            Console.WriteLine(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void ListMembers()
    {
        Console.WriteLine("--- Members ---");
        var members = _memberService.GetAllMembers();
        if (members.Count == 0)
        {
            Console.WriteLine("No members found.");
            return;
        }

        foreach (var member in members)
            Console.WriteLine($"[{member.Id}] {member.Name} ({member.Type})");
    }

    private void BorrowBook()
    {
        Console.WriteLine("--- Borrow Book ---");
        var member = SelectMember();
        if (member == null) return;

        Console.Write("Search book (or press Enter for all available): ");
        string keyword = Console.ReadLine() ?? "";

        var books = _bookService.SearchAvailableBooks(keyword);
        if (books.Count == 0)
        {
            Console.WriteLine("No available books found.");
            return;
        }

        Console.WriteLine("Available Books:");
        for (int i = 0; i < books.Count; i++)
            Console.WriteLine($"{i + 1}. {books[i].Title} by {books[i].Author} (ID: {books[i].Id})");

        Console.Write($"Select (1-{books.Count}, 0 to cancel): ");
        if (!int.TryParse(Console.ReadLine(), out int pick) || pick <= 0 || pick > books.Count)
            return;

        int bookId = books[pick - 1].Id;
        var message = _loanService.TryBorrowBook(member.Id, bookId);
        Console.WriteLine(message);
    }

    private void ReturnBook()
    {
        Console.WriteLine("--- Return Book ---");
        var member = SelectMember();
        if (member == null) return;

        var loans = _loanService.GetActiveLoansForMember(member.Id);
        if (loans.Count == 0)
        {
            Console.WriteLine($"{member.Name} has no borrowed books.");
            return;
        }

        Console.WriteLine("Borrowed Books:");
        for (int i = 0; i < loans.Count; i++)
        {
            var book = _bookService.GetBookById(loans[i].BookId);
            Console.WriteLine($"{i + 1}. {book?.Title} (Due: {loans[i].DueDate:yyyy-MM-dd})");
        }

        Console.Write($"Select book to return (1-{loans.Count}, 0 to cancel): ");
        if (!int.TryParse(Console.ReadLine(), out int pick) || pick <= 0 || pick > loans.Count)
            return;

        int bookId = loans[pick - 1].BookId;
        var message = _loanService.TryReturnBook(bookId);
        Console.WriteLine(message);
    }

    private void ListActiveLoansForMember()
    {
        Console.WriteLine("--- Member Loans ---");
        var member = SelectMember();
        if (member == null) return;

        var loans = _loanService.GetActiveLoansForMember(member.Id);
        if (loans.Count == 0)
        {
            Console.WriteLine($"No active loans for {member.Name}.");
            return;
        }

        Console.WriteLine($"Active Loans for {member.Name}:");
        foreach (var loan in loans)
        {
            var book = _bookService.GetBookById(loan.BookId);
            string overdue = loan.IsOverdue ? " [OVERDUE]" : "";
            Console.WriteLine($"- [{loan.BookId}] '{book?.Title}' (Due: {loan.DueDate:yyyy-MM-dd}){overdue}");
        }
    }

    private void ListOverdueLoans()
    {
        Console.WriteLine("--- Overdue Loans ---");
        var overdueLoans = _loanService.GetOverdueLoans();
        if (overdueLoans.Count == 0)
        {
            Console.WriteLine("No overdue loans.");
            return;
        }

        foreach (var loan in overdueLoans)
        {
            var book = _bookService.GetBookById(loan.BookId);
            var member = _memberService.GetMemberById(loan.MemberId);
            int days = DateOnly.FromDateTime(DateTime.Today).DayNumber - loan.DueDate.DayNumber;
            Console.WriteLine($"- '{book?.Title}' borrowed by {member?.Name} (Due: {loan.DueDate:yyyy-MM-dd}, {days} days late)");
        }
    }

    private Member? SelectMember()
    {
        var members = _memberService.GetAllMembers();
        if (members.Count == 0)
        {
            Console.WriteLine("No members found. Please register a member first.");
            return null;
        }

        Console.WriteLine("Select Member:");
        for (int i = 0; i < members.Count; i++)
            Console.WriteLine($"{i + 1}. {members[i].Name} (ID: {members[i].Id})");

        Console.Write($"Select (1-{members.Count}, 0 to cancel): ");
        if (!int.TryParse(Console.ReadLine(), out int pick) || pick <= 0 || pick > members.Count)
            return null;

        return members[pick - 1];
    }

    private static void InvalidOption()
    {
        Console.WriteLine("Invalid option.");
    }
}
