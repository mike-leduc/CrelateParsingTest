using CrelateParsingTest.Interfaces;
using CrelateParsingTest.Models;
using System.Text;

namespace CrelateParsingTest;

public class ContactValidator : IContactValidator
{
    public void Validate(ContactCollection contacts)
    {
        foreach (var contact in contacts.AllContacts.Select(x => x.record))
        {            
            // If we don't have a Contact we can't validate it, and the assumption is that it isn't valid already
            if (contact.Contact == null)
                continue;

            bool isValid = true;
            StringBuilder sr = new StringBuilder();

            // Going to assume than a Guid.Empty is not valid here, since we set it to Guid.Empty
            // if the Guid is missing or in an invalid format
            if (contact.Contact.Id == Guid.Empty)
            {
                isValid = false;
                sr.AppendLine($"Invalid or missing Id field");
            }
            if (string.IsNullOrEmpty(contact.Contact.FirstName))
            {
                isValid = false;
                sr.AppendLine($"FirstName is missing");
            }
            if (string.IsNullOrEmpty(contact.Contact.LastName))
            {
                isValid = false;
                sr.AppendLine($"LastName is missing");
            }
            if (string.IsNullOrEmpty(contact.Contact.PhoneNumber))
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
