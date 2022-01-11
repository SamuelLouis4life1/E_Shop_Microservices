using AuthenticationJWT.API.Entities;
using AuthenticationJWT.API.Models;
using System;
using System.Collections.Generic;


namespace AuthenticationJWT.API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<ApplicationUser> GetAll();
        ApplicationUser GetById(Guid userId);
        void Register(RegisterRequest model);
        void Update(Guid userId, UpdateRequest model);
        void Delete(Guid userId);
    }
}
