namespace LibraryManagementSystem.Model;

public class Loan
{
    public int Id { get; init; }
    public int BookId { get; init; }
    public int MemberId { get; init; }
    public DateTime LoanDate { get; init; }
    public DateTime DueDate { get; init; }
    public DateTime ReturnDate { get; private set; }
    public bool IsReturned { get; private set; } = false;
    public bool IsOverdue => !IsReturned && DateTime.Today > DueDate.Date;

    public Loan(int id, int bookId, int memberId, DateTime loanDate)
    {
        Id = id;
        BookId = bookId;
        MemberId = memberId;
        LoanDate = loanDate;
        DueDate = loanDate.AddDays(14);
    }

    public void SetReturnDate()
    {
        IsReturned = true;
        ReturnDate = DateTime.Now;
    }
}
