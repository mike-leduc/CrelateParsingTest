using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrelateParsingTest;

public class ContactValidator : IContactValidator
{
    public void Validate(IEnumerable<Contact> contacts)
    {
        foreach (var contact in contacts)
        {
            bool isValid = true;
            StringBuilder sr = new StringBuilder();

            // Going to assume than a Guid.Empty is not valid either?
            if (contact.Id == Guid.Empty)
            {
                isValid = false;
                sr.AppendLine($"Id: {contact.Id} is invalid");
            }
            if (string.IsNullOrEmpty(contact.FirstName))
            {
                isValid = false;
                sr.AppendLine($"FirstName is missing");
            }
            if (string.IsNullOrEmpty(contact.LastName))
            {
                isValid = false;
                sr.AppendLine($"LastName is missing");
            }
            if (string.IsNullOrEmpty(contact.PhoneNumber))
            {
                isValid = false;
                sr.AppendLine($"PhoneNumber is missing");
            }

            if (!isValid)
            {
                contact.ValidationError = sr.ToString();
            }

            contact.IsValid = isValid;
        }
    }
}
