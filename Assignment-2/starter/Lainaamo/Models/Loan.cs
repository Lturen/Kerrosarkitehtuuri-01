namespace Lainaamo.Models;

public class Loan
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public string BorrowerName { get; set; } = string.Empty;
    public DateTime BorrowedAt { get; set; }
    public DateTime? ReturnedAt { get; set; }
}
