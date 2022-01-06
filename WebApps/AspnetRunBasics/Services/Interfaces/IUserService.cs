using AspnetRunBasics.Models.Authenticate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticateRequestModel> Authenticate(AuthenticateRequestModel model);
        IEnumerable<AuthenticateRequestModel> GetAll();
        Task<RegisterRequestModel> GetById(int id);
        Task<RegisterRequestModel> Register(RegisterRequestModel model);
        Task<UpdateRequestModel> UpdateUser(int id, UpdateRequestModel model);
        void DeleteUser(int id);

        //Task<UserModel> CreateUser(UserModel userModel);
    }
}
