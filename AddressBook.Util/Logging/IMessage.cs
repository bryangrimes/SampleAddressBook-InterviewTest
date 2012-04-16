using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddressBook.Util.Logging
{
    public interface IMessage
    {
        void Write(string message, params object[] args);
        void Write(string message);
        void WriteIf(bool eval, string message, params object[] args);
        void WriteIf(bool eval, string message);
    }
}
