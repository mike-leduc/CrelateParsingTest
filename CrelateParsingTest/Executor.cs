using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        foreach (var item in results)
        {
            string validationError = string.IsNullOrEmpty(item.ValidationError) ? string.Empty : $"Validation Error: {item.ValidationError}, ";

            Console.WriteLine($"Contact=> Id: {item.Id}, FirstName: {item.FirstName}, LastName: {item.LastName}, " +
                $"PhoneNumber: {item.PhoneNumber}, IsValid: {item.IsValid}, {validationError}IsDuplicate: {item.IsDuplicate}");
        }
    }
}
