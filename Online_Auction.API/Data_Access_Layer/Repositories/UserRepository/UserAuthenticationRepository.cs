using Data_Access_Layer.Data;
using Data_Access_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories.UserRepository
{
    public class UserAuthenticationRepository : IUserAuthenticationRepository
    {
        private readonly AuctionsDbContextClass _auctionsContext;

        public UserAuthenticationRepository(AuctionsDbContextClass _auctionnContext)
        {
            this._auctionsContext = _auctionnContext;
        }
        public async Task<Users> AuthenticateUser(string EmailID, string Password)
        {
            var user = await _auctionsContext.UsersTable.FirstOrDefaultAsync(u => u.EmailId == EmailID && u.Password == Password);

            return user;
        }
    }
}
