using System;
using System.Diagnostics;
using System.Text;
using log4net;
using log4net.Config;

namespace AddressBook.Util.Logging
{
    internal class LogImplementation : IError, IWarning, IMessage, IVerbose
    {
        private static readonly ILog logger = Log4NetImpl.Logger;

        #region IError Members

        void IError.Write(string message, params object[] args)
        {
            logger.ErrorFormat(message, args);
            Debug.Print("ERROR: " + message, args);
        }


        void IError.Write(string message)
        {
            (this as IError).Write(message, new object[0]);
        }


        public void Write(Exception ex)
        {
            (this as IError).Write(FormatException(ex));
        }

        #endregion

        #region IMessage Members

        void IMessage.Write(string message, params object[] args)
        {
            logger.InfoFormat(message, args);
            Debug.Print("INFO: " + message, args);
        }

        void IMessage.Write(string message)
        {
            (this as IMessage).Write(message, new object[0]);
        }

        void IMessage.WriteIf(bool eval, string message, params object[] args)
        {
            if (eval) (this as IMessage).Write(message, args);
        }

        void IMessage.WriteIf(bool eval, string message)
        {
            if (eval) (this as IMessage).Write(message, new object[0]);
        }

        #endregion

        #region IVerbose Members

        void IVerbose.Write(string message, params object[] args)
        {
            logger.DebugFormat(message, args);
            Debug.Print("DEBUG: " + message, args);
        }

        void IVerbose.Write(string message)
        {
            (this as IVerbose).Write(message, new object[0]);
        }

        void IVerbose.WriteIf(bool eval, string message, params object[] args)
        {
            if (eval) (this as IVerbose).Write(message, args);
        }

        void IVerbose.WriteIf(bool eval, string message)
        {
            if (eval) (this as IVerbose).Write(message);
        }

        #endregion

        #region IWarning Members

        void IWarning.Write(string message, params object[] args)
        {
            logger.WarnFormat(message, args);
            Debug.Print("WARNING: " + message, args);
        }


        void IWarning.Write(string message)
        {
            (this as IWarning).Write(message, new object[0]);
        }

        #endregion

        private string FormatException(Exception ex)
        {
            var sb = new StringBuilder();
            sb.AppendLine(String.Format("*** {0} Exception ***", ex));
            sb.AppendLine(String.Format("* {0}", ex.Message));
            sb.AppendLine();
            sb.AppendLine(String.Format("Stacktrace:"));
            sb.AppendLine(String.Format("{0}", ex.StackTrace));

            if (ex.InnerException != null)
                sb.AppendLine(String.Format("Inner Exception {0}", FormatException(ex.InnerException)));

            return sb.ToString();
        }
    }
}