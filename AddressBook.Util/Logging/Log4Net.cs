using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;
using log4net.Core;

namespace AddressBook.Util.Logging
{
    class Log4NetImpl
    {
        private static ILog _logger;

        internal static ILog Logger
        {
            get
            {
                if (_logger == null)
                {
                    XmlConfigurator.Configure(); // log4net requires configuration before using
                    _logger = LogManager.GetLogger(typeof(LogImplementation));
                }
                return _logger;
            }
        }


    }
}
