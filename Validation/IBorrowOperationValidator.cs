namespace LibraryManagementSystem.Validation;

using LibraryManagementSystem.Model;

public interface IBorrowOperationValidator
{
    ValidationResult Validate(Member member, Book book, IReadOnlyList<Loan> memberActiveLoans);
}
