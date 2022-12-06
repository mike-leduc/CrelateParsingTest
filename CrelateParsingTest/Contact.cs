using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrelateParsingTest;

public class Contact
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsValid { get;set; }
    public bool IsDuplicate { get; set; } // Can't know that here
    public string? ValidationError { get; set; }
}

