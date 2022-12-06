using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CrelateParsingTest;

public class ContactParser : IContactParser
{
    public IEnumerable<Contact> ParseRecords(string contacts)
    {
        // gotta have data or we can't do anything
        if (string.IsNullOrEmpty(contacts))
            return Enumerable.Empty<Contact>();

        string[] records = contacts.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        // We are assuming that we have a header in this data
        // Based on that assumption, if the length is 1 then all we have is a header, so no processing can occur.
        if (records.Length < 2)
            return Enumerable.Empty<Contact>();

        // Use this to aid in duplicate detection
        List<Contact> result = new List<Contact>();

        for (int i = 1; i < records.Length; i++)
        {
            string record = records[i];
            if (string.IsNullOrEmpty(record)) 
                continue;

            var columns = record.Split(',', StringSplitOptions.TrimEntries);

            // Ideal length is 4: Id,FirstName,LastName,PhoneNumber
            Contact? contact = CreateContact(columns);
            if (contact != null)
            {
                result.Add(contact);
            }
        }

        return CheckForDuplicates(result);
    }

    private IEnumerable<Contact> CheckForDuplicates(List<Contact> contacts)
    {
        // Detect duplicates
        var duplicates = contacts
            .Select((t, i) => new { Index = i, Contact = t })
            .Where(x => !string.IsNullOrEmpty(x.Contact.PhoneNumber))
            .GroupBy(g => g.Contact.PhoneNumber)
            .Where(g => g.Count() > 1);

        // Don't do anything if we don't have any duplicats
        if (duplicates.Any())
        {
            List<int> duplicateIndexes = new List<int>();
            foreach (var item in duplicates)
            {
                duplicateIndexes.AddRange(item.Select(i => i.Index));
            }

            foreach (var index in duplicateIndexes)
            {
                contacts[index].IsDuplicate = true;
            }
        }

        return contacts;
    }

    private Contact? CreateContact(string[] contactInfo)
    {
        // It would take much more advanced logic to deal with records that have too many columns,
        // and columns that don't match the expected data type.
        // It could be done, but probably not worth it for this exercise
        if (contactInfo.Length != 4)
        {
            Console.WriteLine($"Invalid record format: {string.Join(",", contactInfo)}. Expected 4 columns, Actual: {contactInfo.Length}");
            return null;
        }

        if (!Guid.TryParse(contactInfo[0], out Guid guid))
            guid = Guid.NewGuid();

        Contact result = new Contact
        {
            Id = guid,
            FirstName = contactInfo[1],
            LastName = contactInfo[2],
            PhoneNumber = string.IsNullOrEmpty(contactInfo[3]) ? "" : NormalizePhoneNumber(contactInfo[3]) // Not sure what to do here if a phone number is missing
        };

        return result;
    }

    private static string NormalizePhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber))
            return phoneNumber;

        // Given more time I would probably use RegEx to do this work with some better pattern matching
        return phoneNumber.Replace("(", string.Empty).Replace(")", string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty).Trim();
    }
}
