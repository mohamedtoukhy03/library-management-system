namespace LibraryManagementSystem.Service;

using LibraryManagementSystem.Model;
using LibraryManagementSystem.Repository;
using LibraryManagementSystem.Validation;

public class LoanService(
    IBookRepository bookRepository,
    IMemberRepository memberRepository,
    ILoanRepository loanRepository,
    IBorrowOperationValidator borrowValidator) : ILoanService
{
    private readonly IBookRepository _bookRepo = bookRepository;
    private readonly IMemberRepository _memberRepo = memberRepository;
    private readonly ILoanRepository _loanRepo = loanRepository;
    private readonly IBorrowOperationValidator _borrowValidator = borrowValidator;
    private int _nextLoanId = 1;

    public bool TryBorrowBook(int memberId, int bookId, out string message)
    {
        var member = _memberRepo.GetById(memberId);
        if (member == null)
        {
            message = "Member not found.";
            return false;
        }

        var book = _bookRepo.GetById(bookId);
        if (book == null)
        {
            message = "Book not found.";
            return false;
        }

        var activeLoans = _loanRepo.GetActiveByMemberId(memberId);
        var validation = _borrowValidator.Validate(member, book, activeLoans);
        if (!validation.IsValid)
        {
            message = validation.ErrorMessage ?? "";
            return false;
        }

        var loan = new Loan(_nextLoanId++, book.Id, member.Id, DateTime.Now);
        book.MarkAsBorrowed();
        _loanRepo.Add(loan);

        message = $"'{book.Title}' successfully borrowed by {member.Name}! Due date: {loan.DueDate:yyyy-MM-dd}.";
        return true;
    }

    public bool TryReturnBook(int bookId, out string message)
    {
        var loan = _loanRepo.GetActiveByBookId(bookId);
        if (loan == null)
        {
            message = "No active loan found for this book.";
            return false;
        }

        var book = _bookRepo.GetById(bookId);
        if (book == null)
        {
            message = "Book record not found.";
            return false;
        }

        loan.SetReturnDate();
        book.MarkAsReturned();

        message = $"'{book.Title}' successfully returned and marked as available.";
        return true;
    }

    public IReadOnlyList<Loan> GetActiveLoansForMember(int memberId) => _loanRepo.GetActiveByMemberId(memberId);

    public IReadOnlyList<Loan> GetOverdueLoans() => _loanRepo.GetAllOverdue();

    public IReadOnlyList<Loan> GetAllActiveLoans() => _loanRepo.GetAllActive();
}
