using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories.UserRepository
{
    public interface IUserAuthenticationRepository
    {
        Task<Users> AuthenticateUser(string EmailID, string Password);
    }
}
