using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddressBook.Util.Logging
{
    public interface IWarning
    {

        void Write(string message);
        void Write(string message, params object[] args);

    }
}
