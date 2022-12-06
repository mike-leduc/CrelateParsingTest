using CrelateParsingTest.Interfaces;

namespace CrelateParsingTest;

public class Executor
{
    private IContactParser Parser { get; }
    private IContactValidator Validator { get; }

    public Executor(IContactParser contactParser, IContactValidator contactValidator)
    {
        Parser = contactParser;
        Validator = contactValidator;
    }

    public void Execute(string contacts)
    {
        var results = Parser.ParseRecords(contacts);
        Validator.Validate(results);

        foreach (var item in results.AllContacts)
        {
            string recordValue = "contact: " + (item.record.Contact == null ? item.record.RawRecord : $"{item.record.Contact}");
            string validationError = string.IsNullOrEmpty(item.record.ValidationError) ? string.Empty : $", Validation Error: {item.record.ValidationError}";

            Console.WriteLine($"Contact=> RowNumber: {item.rowIndex}, {recordValue}, isValid: {item.record.IsValid}, isDuplicate: {item.record.IsDuplicate}{validationError}");
        }
    }
}
