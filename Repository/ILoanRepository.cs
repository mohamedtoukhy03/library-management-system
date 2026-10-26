namespace LibraryManagementSystem.Repository;

using LibraryManagementSystem.Model;

public interface ILoanRepository
{
    void Add(Loan loan);
    Loan? GetActiveByBookId(int bookId);
    IReadOnlyList<Loan> GetActiveByMemberId(int memberId);
    IReadOnlyList<Loan> GetAllActive();
    IReadOnlyList<Loan> GetAllOverdue();
}

