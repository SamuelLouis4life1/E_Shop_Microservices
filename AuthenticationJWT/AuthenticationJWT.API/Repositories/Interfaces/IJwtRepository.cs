using AuthenticationJWT.API.Entities;
using System;


namespace AuthenticationJWT.API.Repositories.Interfaces
{
    public interface IJwtRepository
    {
        public string GenerateToken(ApplicationUser applicationUser);
        public Guid? ValidateToken(string token);
    }
}
