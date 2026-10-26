namespace LibraryManagementSystem.Repository;

using LibraryManagementSystem.Model;

public class InMemoryLoanRepository : ILoanRepository
{
    private readonly List<Loan> _loans = [];

    public void Add(Loan loan)
    {
        _loans.Add(loan);
    }

    public Loan? GetActiveByBookId(int bookId) => _loans.FirstOrDefault(loan => loan.BookId == bookId && !loan.IsReturned);

    public IReadOnlyList<Loan> GetActiveByMemberId(int memberId)
    {
        return _loans
            .Where(loan => loan.MemberId == memberId && !loan.IsReturned)
            .OrderBy(loan => loan.DueDate)
            .ToList()
            .AsReadOnly();
    }

    public IReadOnlyList<Loan> GetAllActive()
    {
        return _loans
            .Where(loan => !loan.IsReturned)
            .ToList()
            .AsReadOnly();
    }

    public IReadOnlyList<Loan> GetAllOverdue()
    {
        return _loans
            .Where(loan => loan.IsOverdue)
            .ToList()
            .AsReadOnly();
    }
}

