using System;
using System.Globalization;


namespace AuthenticationJWT.API.Exceptions
{
    public class AuthenticationException : ApplicationException
    {
        //public AuthenticationException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
        //{
        //}
        public AuthenticationException() : base() { }

        public AuthenticationException(string message) : base(message) { }

        public AuthenticationException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
