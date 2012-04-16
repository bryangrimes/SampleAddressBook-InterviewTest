using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddressBook.Util.Logging
{
    public static class Logger
    {
        public static IMessage Message
        {
            get
            {
                return new LogImplementation();
            }
        }

        public static IWarning Warning
        {
            get
            {
                return new LogImplementation();
            }
        }

        public static IError Error
        {
            get
            {
                return new LogImplementation();
            }
        }

        public static IVerbose Verbose
        {
            get
            {
                return new LogImplementation();
            }
        }
    }
}
