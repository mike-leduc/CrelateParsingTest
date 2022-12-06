namespace CrelateParsingTest.Models;

public class ContactRecord
{
    public int RowNumber { get; set; }
    public string? RawRecord { get; set; }
    public Contact? Contact { get; set; }
    public bool IsValid { get; set; }
    public string? ValidationError { get; set; }
    public bool IsDuplicate { get; set; }
}
