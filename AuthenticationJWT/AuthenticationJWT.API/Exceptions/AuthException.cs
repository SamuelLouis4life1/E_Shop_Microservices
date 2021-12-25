using System;
using System.Globalization;


namespace AuthenticationJWT.API.Exceptions
{
    public class AuthException : ApplicationException
    {
        //public AuthenticationException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
        //{
        //}
        public AuthException() : base() { }

        public AuthException(string message) : base(message) { }

        public AuthException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
