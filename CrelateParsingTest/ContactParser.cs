using CrelateParsingTest.Extensions;
using CrelateParsingTest.Interfaces;
using CrelateParsingTest.Models;

namespace CrelateParsingTest;

public class ContactParser : IContactParser
{
    public ContactCollection ParseRecords(string contacts)
    {
        ContactCollection result = new ContactCollection();

        // gotta have data or we can't do anything
        if (string.IsNullOrEmpty(contacts))
            return result;

        // Split on NewLines
        string[] records = contacts.Split(Environment.NewLine, StringSplitOptions.None);

        // We are assuming that we have a header in this data
        // Based on that assumption, if the length is 1 then all we have is a header, so no processing can occur.
        if (records.Length < 2)
            return result;        

        for (int index = 1; index < records.Length; index++)
        {
            string record = records[index];
            if (string.IsNullOrEmpty(record)) 
                continue;

            var columns = record.Split(',', StringSplitOptions.TrimEntries); // Trim whitespace from entries

            // Correct? length is 4: Id,FirstName,LastName,PhoneNumber
            Contact? contact = CreateContact(columns, out string? errorMessage);
            result.AddContact(index, record, contact, errorMessage);
        }

        return CheckForDuplicates(result);
    }

    private ContactCollection CheckForDuplicates(ContactCollection contacts)
    {
        // Detect duplicates
        var duplicates = contacts.Contacts
            .Select(t => new { Index = t.rowIndex, Contact = t.contact })
            .Where(x => !string.IsNullOrEmpty(x.Contact.PhoneNumber))
            .GroupBy(g => g.Contact.PhoneNumber)
            .Where(g => g.Count() > 1);

        // Don't do anything if we don't have any duplicates
        if (duplicates.Any())
        {
            List<int> duplicateIndexes = new List<int>();
            foreach (var item in duplicates)
            {
                duplicateIndexes.AddRange(item.Select(i => i.Index));
            }

            foreach (var index in duplicateIndexes)
            {
                var record = contacts[index];
                if (record != null)
                    record.IsDuplicate = true;
            }
        }

        return contacts;
    }

    private Contact? CreateContact(string[] contactInfo, out string? errorMessage)
    {
        errorMessage = null;
        // It would take much more advanced logic to deal with records that have too many columns,
        // and columns that don't match the expected data type.
        // It could be done, but probably not worth it for this exercise
        if (contactInfo.Length != 4)
        {
            errorMessage = $"Invalid record format: Expected 4 columns, Actual # of columns: {contactInfo.Length}";
            //Console.WriteLine(errorMessage); //Would advocate logging these somewhere

            return null;
        }

        if (!Guid.TryParse(contactInfo[0], out Guid guid))
            guid = Guid.Empty;

        return new Contact
        (
            guid,
            contactInfo[1],
            contactInfo[2],
            string.IsNullOrEmpty(contactInfo[3]) ? "" : contactInfo[3].NormalizePhoneNumber() // Not sure what to do here if a phone number is missing
        );
    }
}
