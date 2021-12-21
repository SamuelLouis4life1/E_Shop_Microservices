using System;


namespace AuthenticationJWT.API.Authorization
{
    public class AllowAnonymousAttribute
    {
        [AttributeUsage(AttributeTargets.Method)]
        public class AllowAnonymousAttribute : Attribute
        { }
    }
}
