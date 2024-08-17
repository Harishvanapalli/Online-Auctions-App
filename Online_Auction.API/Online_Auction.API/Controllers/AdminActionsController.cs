using AutoMapper;
using Business_Logic_Layer.Services.AdminActionsService;
using Business_Logic_Layer.Services.AuctionsService;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories.AdminActionsRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Auction.API.Dto_s;

namespace Online_Auction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminActionsController : ControllerBase
    {
        private readonly IAdminActionsService _adminActionsService;
        private readonly IMapper _mapper;

        public AdminActionsController(IAdminActionsService _adminActionsService, IMapper _mapper)
        {
            this._adminActionsService = _adminActionsService;
            this._mapper = _mapper;
        }

        [HttpGet("getUsersParticipatedAuctions")]
        public async Task<ActionResult<ApiResponse<ICollection<BidsDto>>>> GetUsersParticipatedAuctions()
        {
            var response = new ApiResponse<ICollection<BidsDto>>();

            try
            {
                var serviceResponse = await _adminActionsService.GerUsersParticipatedAuctions();

                if (serviceResponse.IsSuccess && serviceResponse.Result != null)
                {
                    var UserBids = _mapper.Map<ICollection<BidsDto>>(serviceResponse.Result);
                    response.Result = UserBids;
                    response.Message = $"All Bids of Users Retrived Successfully";
                    response.IsSuccess = true;
                }
                else
                {
                    response.Message = serviceResponse.Message;
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message.ToString()}";
                return response;
            }
        }

        [HttpGet("getUserStartedAuctions")]
        public async Task<ActionResult<ApiResponse<ICollection<Auctions>>>> GetUsersSratedAuctions()
        {
            var response = new ApiResponse<ICollection<Auctions>>();

            try
            {
                var serviceResponse = await _adminActionsService.GetAuctionsStartedByUsers();

                response.Result = serviceResponse.Result;
                response.Message = $"All Auctions of Users Retrived Successfully";
                response.IsSuccess = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message.ToString()}";
                return response;
            }
        }

        [HttpDelete("deleteAuctionById/{id}")]
        public async Task<ActionResult<ApiResponse<Auctions>>> DeleteAuctionByID(int id)
        {
            var response = new ApiResponse<Auctions>();
            try
            {
                var serviceResponse = await _adminActionsService.DeleteAuctionByID(id);

                response.Result = serviceResponse.Result;
                response.Message = serviceResponse.Message;
                response.IsSuccess = serviceResponse.IsSuccess;

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message.ToString()}";
                return response;
            }
        }

        [HttpGet("getallActiveAuctions")]
        public async Task<ActionResult<ApiResponse<ICollection<AuctionsDto>>>> GetAllActiveAuctions()
        {
            var response = new ApiResponse<ICollection<AuctionsDto>>();
            try
            {
                var serviceRespone = await _adminActionsService.GetAllActiveAuctions();

                if(serviceRespone.IsSuccess && serviceRespone.Result != null)
                {
                    response.Result = _mapper.Map<ICollection<AuctionsDto>>(serviceRespone.Result);
                    response.Message = "All Active Auctions Retrived Successfully";
                    response.IsSuccess = true;
                }
                else
                {
                    response.Message = serviceRespone.Message;
                }

                return response;
            }catch(Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message.ToString()}";
                return response;
            }
        }

        [HttpPut("updateUser")]
        public async Task<ActionResult<ApiResponse<Users>>> Suspendser([FromBody] Users user)
        {
            var response = new ApiResponse<Users>();
            try
            {
                var serviceRespone = await _adminActionsService.SuspendUserByID(user);

                response.Result = serviceRespone.Result;
                response.Message = serviceRespone.Message;
                response.IsSuccess = serviceRespone.IsSuccess;

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message.ToString()}";
                return response;
            }
        }

        [HttpGet("getAllUsersDetails")]
        public async Task<ActionResult<ApiResponse<ICollection<Users>>>> GetAllUsersDetails()
        {
            var response = new ApiResponse<ICollection<Users>>();

            try
            {
                var serviceResponse = await _adminActionsService.GetAllUsers();

                response.Result = serviceResponse.Result;
                response.Message = serviceResponse.Message;
                response.IsSuccess = serviceResponse.IsSuccess;

                return response;
            }catch(Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message.ToString()}";
                return response;
            }
        }

    }
}
