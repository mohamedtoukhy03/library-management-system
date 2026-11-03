namespace LibraryManagementSystem.Model;

public class Loan
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int MemberId { get; set; }
    public DateOnly LoanDate { get; set; }
    public DateOnly DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public bool IsReturned => ReturnDate != null;
    public bool IsOverdue => !IsReturned && DateOnly.FromDateTime(DateTime.Today) > DueDate;

    public Loan(int id, int bookId, int memberId, DateOnly loanDate)
    {
        Id = id;
        BookId = bookId;
        MemberId = memberId;
        LoanDate = loanDate;
        DueDate = loanDate.AddDays(14);
    }
}
