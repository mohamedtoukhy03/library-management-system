namespace LibraryManagementSystem.Validation;

using LibraryManagementSystem.Model;

public class BorrowOperationValidator : IBorrowOperationValidator
{
    public ValidationResult Validate(Member member, Book book, IReadOnlyList<Loan> memberActiveLoans)
    {
        if (!book.IsAvailable)
            return ValidationResult.Failure($"'{book.Title}' is already borrowed.");

        if (memberActiveLoans.Any(loan => loan.IsOverdue))
            return ValidationResult.Failure($"Borrowing rejected: {member.Name} currently has overdue book(s).");

        int activeCount = memberActiveLoans.Count(loan => !loan.IsReturned);
        if (activeCount >= member.MaxLoans)
            return ValidationResult.Failure($"Borrowing rejected: {member.Name} has reached the limit of {member.MaxLoans} active loans.");

        return ValidationResult.Success();
    }
}
