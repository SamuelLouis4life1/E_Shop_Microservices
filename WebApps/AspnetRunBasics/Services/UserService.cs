using AspnetRunBasics.Models.Authenticate;
using AspnetRunBasics.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public class UserService : IUserService
    {
        public AuthenticateRequestModel Authenticate(AuthenticateRequestModel model)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AuthenticateRequestModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public RegisterRequestModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Register(RegisterRequestModel model)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(int id, UpdateRequestModel model)
        {
            throw new NotImplementedException();
        }
    }
}
