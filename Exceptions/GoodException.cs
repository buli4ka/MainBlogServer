using System;
using System.Globalization;

namespace Server.Exceptions
{
    public class GoodException : Exception
    {
        public GoodException() : base()
        {
        }

        public GoodException(string message) : base(message)
        {
        }

        public GoodException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}