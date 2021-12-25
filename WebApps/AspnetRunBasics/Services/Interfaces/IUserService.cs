using AspnetRunBasics.Models.Authenticate;
using System.Collections.Generic;


namespace AspnetRunBasics.Services.Interfaces
{
    public interface IUserService
    {
        AuthenticateRequestModel Authenticate(AuthenticateRequestModel model);
        IEnumerable<AuthenticateRequestModel> GetAll();
        RegisterRequestModel GetById(int id);
        void Register(RegisterRequestModel model);
        void UpdateUser(int id, UpdateRequestModel model);
        void DeleteUser(int id);

        //Task<UserModel> GetUser(string userName);
        //Task<UserModel> CreateUser(UserModel userModel);
        //Task<UserModel> UpdateUser(UserModel userModel);
    }
}
