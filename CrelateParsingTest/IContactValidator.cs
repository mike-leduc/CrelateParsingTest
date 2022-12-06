using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrelateParsingTest;

public interface IContactValidator
{
    void Validate(IEnumerable<Contact> contacts);
}
