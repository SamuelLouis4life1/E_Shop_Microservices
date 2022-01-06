using Shopping.Aggregator.Models.Authenticate;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Shopping.Aggregator.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticateRequestModel> Authenticate(AuthenticateRequestModel model);
        Task<IEnumerable<AuthenticateRequestModel>> GetAllUsers();
        Task<RegisterRequestModel> GetById(int id);
        Task<RegisterRequestModel> Register(RegisterRequestModel model);
        Task<UpdateRequestModel> UpdateUser(int id, UpdateRequestModel model);
        void DeleteUser(int id);
    }
}
