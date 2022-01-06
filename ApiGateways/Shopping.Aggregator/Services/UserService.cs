using Shopping.Aggregator.Models.Authenticate;
using Shopping.Aggregator.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public class UserService : IUserService
    {
        public Task<AuthenticateRequestModel> Authenticate(AuthenticateRequestModel model)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AuthenticateRequestModel>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<RegisterRequestModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<RegisterRequestModel> Register(RegisterRequestModel model)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateRequestModel> UpdateUser(int id, UpdateRequestModel model)
        {
            throw new NotImplementedException();
        }
    }
}
