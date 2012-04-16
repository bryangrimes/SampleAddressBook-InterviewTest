using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddressBook.Util.Logging
{
    public interface IError
    {
        void Write(string message);
        void Write(string message, params object[] args);
        void Write(Exception ex);

    }
}
