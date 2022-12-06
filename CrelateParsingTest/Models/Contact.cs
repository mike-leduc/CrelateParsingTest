namespace CrelateParsingTest.Models;

// Use record here, since this is Immutable. Also like the fact that .ToString() is overloaded and dumps out the values for you
public record Contact
(
    Guid Id, string? FirstName, string? LastName, string? PhoneNumber
);
