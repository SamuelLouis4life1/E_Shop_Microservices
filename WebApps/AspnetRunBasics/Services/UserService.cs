using AspnetRunBasics.Extensions;
using AspnetRunBasics.Models.Authenticate;
using AspnetRunBasics.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _userService;

        public UserService(HttpClient userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService)); ;
        }

        public Task<AuthenticateRequestModel> Authenticate(AuthenticateRequestModel model)
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

        public async Task<RegisterRequestModel> GetById(int id)
        {
            var response = await _userService.GetAsync($"/users/{id}");
            return await response.ReadContentAs<RegisterRequestModel>();
        }

        public async Task<RegisterRequestModel> Register(RegisterRequestModel model)
        {
            var response = await _userService.PostAsJson($"/register", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<RegisterRequestModel>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }

        public async Task<UpdateRequestModel> UpdateUser(int id, UpdateRequestModel model)
        {
            var response = await _userService.PostAsJson($"/Users/{id}", model);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<UpdateRequestModel>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
    }
}
