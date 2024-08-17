using Business_Logic_Layer.Models;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories.UserRepository;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.UsersService
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserAuthenticationRepository _userAuthenticationRepository;

        private const int Expires_In = 30;
        public const string SecurityKey = "IamCreatingtheJwtTokenAuction123";

        public UserAuthenticationService(IUserAuthenticationRepository _userAuthenticationRepository)
        {
            this._userAuthenticationRepository = _userAuthenticationRepository;
        }
        public async Task<AuthenticationResult> AuthenticateUser(string EmailID, string Password)
        {
            var user = await _userAuthenticationRepository.AuthenticateUser(EmailID, Password);
            if (user == null)
            {
                return null;
            }

            var result = CreateToken(user);

            return result;
        }

        public AuthenticationResult CreateToken(Users user)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            var claimsIdentity = new ClaimsIdentity(new List<Claim>
                {
                new Claim("IsSuspended", user.Suspend.ToString()),
                new Claim(ClaimTypes.Email, user.EmailId),
                new Claim(ClaimTypes.Role, user.Role)
                });

            var ExpiresInTime = DateTime.Now.AddMinutes(Expires_In);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = ExpiresInTime,
                SigningCredentials = credentials
            };

            var Securitytoken = jwtSecurityTokenHandler.CreateToken(tokenDescripter);

            var token = jwtSecurityTokenHandler.WriteToken(Securitytoken);

            var response = new AuthenticationResult
            {
                UserName = user.UserName,
                Token = token,
                Expires_In = (int)ExpiresInTime.Subtract(DateTime.Now).TotalMinutes
            };

            return response;
        }
    }
}
