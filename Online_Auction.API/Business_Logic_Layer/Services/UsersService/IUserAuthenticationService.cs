using Business_Logic_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.UsersService
{
    public interface IUserAuthenticationService
    {
        Task<AuthenticationResult> AuthenticateUser(string EmailID, string Password);

    }
}
