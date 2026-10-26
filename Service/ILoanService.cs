namespace LibraryManagementSystem.Service;

using LibraryManagementSystem.Model;

public interface ILoanService
{
    bool TryBorrowBook(int memberId, int bookId, out string message);
    bool TryReturnBook(int bookId, out string message);

    IReadOnlyList<Loan> GetActiveLoansForMember(int memberId);
    IReadOnlyList<Loan> GetOverdueLoans();
    IReadOnlyList<Loan> GetAllActiveLoans();
}
