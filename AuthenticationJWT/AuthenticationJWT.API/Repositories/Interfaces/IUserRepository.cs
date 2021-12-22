using AuthenticationJWT.API.Entities;
using AuthenticationJWT.API.Models;
using System.Collections.Generic;


namespace AuthenticationJWT.API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Register(RegisterRequest model);
        void Update(int id, UpdateRequest model);
        void Delete(int id);
    }
}
