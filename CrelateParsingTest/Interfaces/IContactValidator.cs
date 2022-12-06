using CrelateParsingTest.Models;

namespace CrelateParsingTest.Interfaces;

public interface IContactValidator
{
    void Validate(ContactCollection contacts);
}
