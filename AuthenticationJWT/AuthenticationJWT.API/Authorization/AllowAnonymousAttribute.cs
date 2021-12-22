using System;


namespace AuthenticationJWT.API.Authorization
{
    [AttributeUsage(AttributeTargets.Method)]

    public class AllowAnonymousAttribute : Attribute
    {

    }
}
