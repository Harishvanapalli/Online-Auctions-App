using Business_Logic_Layer.Services.UsersService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Auction.API.Dto_s;

namespace Online_Auction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersAuthenticationController : ControllerBase
    {

        private readonly IUserAuthenticationService _userAuthenticationService;

        public UsersAuthenticationController(IUserAuthenticationService _userAuthenticationService)
        {
            this._userAuthenticationService = _userAuthenticationService;
        }

        [HttpPost("UserLogin")]

        public async Task<ActionResult<LoginResponseDto>> UserLogin([FromBody] UserDto User)
        {
            var result = await _userAuthenticationService.AuthenticateUser(User.EmailID, User.Password);

            if(result != null)
            {
                return Ok(new LoginResponseDto { UserName = result.UserName, Token = result.Token, Expires_In = result.Expires_In });
            }
            return Unauthorized(new { Message = "InValid Credentials" });
        }
    }
}
