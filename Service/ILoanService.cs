namespace LibraryManagementSystem.Service;

using LibraryManagementSystem.Model;

public interface ILoanService
{
    string TryBorrowBook(int memberId, int bookId);
    string TryReturnBook(int bookId);
    IReadOnlyList<Loan> GetActiveLoansForMember(int memberId);
    IReadOnlyList<Loan> GetOverdueLoans();
    IReadOnlyList<Loan> GetAllActiveLoans();
}
