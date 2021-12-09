using System;
using System.Globalization;

namespace Server.Exceptions
{
    public class ForbbidenEcxeption : Exception
    {
        public ForbbidenEcxeption() : base()
        {
        }

        public ForbbidenEcxeption(string message) : base(message)
        {
        }

        public ForbbidenEcxeption(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}