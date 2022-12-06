using CrelateParsingTest.Models;

namespace CrelateParsingTest;

public class ContactCollection
{
    // Store all information about a Contact here
    private Dictionary<int, ContactRecord> ContactRecords { get; } = new Dictionary<int, ContactRecord>();

    public IEnumerable<(int rowIndex, ContactRecord record)> AllContacts => ContactRecords.Values.Select(x => (x.RowNumber, x));

    // Only return Contacts that we could construct. This is the true DTO for a Contact
    public IEnumerable<(int rowIndex, Contact contact)> Contacts => GetContact().Select(x => (x.RowNumber, x.Contact!));

    public ContactRecord? this[int index]
    {
        get
        {
            if (ContactRecords.TryGetValue(index, out var record))
                return record;

            return null;
        }
    }

    public ContactRecord AddContact(int rowNumber, string rawRecord, Contact? contact, string? errorMessage = null)
    {
        ContactRecord result = new ContactRecord 
        { 
            RowNumber = rowNumber, 
            RawRecord = rawRecord, 
            Contact = contact,
            ValidationError= errorMessage
        };

        ContactRecords.Add(rowNumber, result);

        return result;
    }

    public IEnumerable<ContactRecord> GetContact()
    {
        return ContactRecords.Where(w => w.Value.Contact != null).Select(x => x.Value);
    }
}
