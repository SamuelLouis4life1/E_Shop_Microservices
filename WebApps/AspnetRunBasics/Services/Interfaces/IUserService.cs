using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services.Interfaces
{
    public interface IUserService
    {
        //Task<UserModel> GetUser(string userName);
        //Task<UserModel> CreateUser(UserModel userModel);
        //Task<UserModel> UpdateUser(UserModel userModel);
        Task DeleteUser(string userName);
    }
}
