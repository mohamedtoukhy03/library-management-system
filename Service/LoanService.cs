namespace LibraryManagementSystem.Service;

using LibraryManagementSystem.Model;
using LibraryManagementSystem.Repository;

public class LoanService(
    IBookRepository bookRepository,
    IMemberRepository memberRepository,
    ILoanRepository loanRepository) : ILoanService
{
    private readonly IBookRepository _bookRepo = bookRepository;
    private readonly IMemberRepository _memberRepo = memberRepository;
    private readonly ILoanRepository _loanRepo = loanRepository;
    private int _nextLoanId = 1;

    public string TryBorrowBook(int memberId, int bookId)
    {
        var member = _memberRepo.GetById(memberId);
        if (member == null)
            return "Member not found.";

        var book = _bookRepo.GetById(bookId);
        if (book == null)
            return "Book not found.";

        if (!book.IsAvailable)
            return $"'{book.Title}' is already borrowed.";

        var memberOverdueActiveLoans = _loanRepo.GetActiveOverdueByMemberId(memberId);
        var memberActiveLoans = _loanRepo.GetActiveByMemberId(memberId);

        if (memberOverdueActiveLoans.Count >= 1)
            return $"Borrowing rejected: {member.Name} currently has overdue book.";

        if (memberActiveLoans.Count >= member.MaxLoans)
            return $"Borrowing rejected: {member.Name} has reached the limit of {member.MaxLoans} active loans.";

        var loan = new Loan(_nextLoanId++, book.Id, member.Id, DateOnly.FromDateTime(DateTime.Today));
        book.IsAvailable = false;
        _loanRepo.Add(loan);

        return $"'{book.Title}' successfully borrowed by {member.Name}! Due date: {loan.DueDate:yyyy-MM-dd}.";
    }

 

    public string TryReturnBook(int bookId)
    {
        var loan = _loanRepo.GetActiveByBookId(bookId);
        if (loan == null)
            return "No active loan found for this book.";

        var book = _bookRepo.GetById(bookId);
        if (book == null)
            return "Book record not found.";

        loan.ReturnDate = DateTime.Now;
        book.IsAvailable = true;

        return $"'{book.Title}' successfully returned and marked as available.";
    }

    public IReadOnlyList<Loan> GetActiveLoansForMember(int memberId) => _loanRepo.GetActiveByMemberId(memberId);

    public IReadOnlyList<Loan> GetOverdueLoans() => _loanRepo.GetAllOverdue();

    public IReadOnlyList<Loan> GetAllActiveLoans() => _loanRepo.GetAllActive();
}
