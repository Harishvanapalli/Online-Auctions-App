using AutoMapper;
using Business_Logic_Layer.Services.AuctionsService;
using Data_Access_Layer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Online_Auction.API.Dto_s;

namespace Online_Auction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionsController : ControllerBase
    {
        private readonly IAuctionsService _auctionsService;
        private readonly IMapper _mapper;

        public AuctionsController(IAuctionsService _auctionsService, IMapper _mapper)
        {
            this._auctionsService = _auctionsService;
            this._mapper = _mapper;
        }

        [HttpGet("getAllActiveAuctions")]
        public async Task<ActionResult<ApiResponse<ICollection<AuctionsDto>>>> GetAllActiveAuctions()
        {
            var response = new ApiResponse<ICollection<AuctionsDto>>();
            try
            {
                var serviceResponse = await _auctionsService.GetAllActiveAuctions();

                if (serviceResponse.IsSuccess && serviceResponse.Result != null)
                {
                    var ActiveAuctions = _mapper.Map<ICollection<AuctionsDto>>(serviceResponse.Result);
                    response.Result = ActiveAuctions;
                    response.Message = "All Active Auctions Retrived Successfully";
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

        [HttpPost("startAuction")]
        public async Task<ActionResult<ApiResponse<Auctions>>> StartAuctionforProduct([FromBody] CreateAuctionDto auctionDto)
        {
            var response = new ApiResponse<Auctions>();
            try
            {
                if (ModelState.IsValid)
                {
                    var Auction = _mapper.Map<Auctions>(auctionDto);
                    var serviceResponse = await _auctionsService.CreateAuction(Auction);

                    response.Result = serviceResponse.Result;
                    response.Message = serviceResponse.Message;
                    response.IsSuccess = serviceResponse.IsSuccess;

                    return response;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = string.Join("; ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));

                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message.ToString();
                return response;
            }
        }

        [HttpPost("userParticipationInAuction")]
        public async Task<ActionResult<ApiResponse<Bids>>> UserParticipationInAuction([FromBody] CreateUserBidDto userBidDto)
        {
            var response = new ApiResponse<Bids>();
            try
            {
                if (ModelState.IsValid)
                {
                    var UserBid = _mapper.Map<Bids>(userBidDto);
                    var serviceResponse = await _auctionsService.UserparticipationInAuction(UserBid);

                    response.Result = serviceResponse.Result;
                    response.Message = serviceResponse.Message;
                    response.IsSuccess = serviceResponse.IsSuccess;

                    return response;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = string.Join("; ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));

                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message.ToString();
                return response;
            }
        }

        [HttpPost("createProduct")]
        public async Task<ActionResult<ApiResponse<Products>>> CreateProduct([FromBody] CreateProductDto productDto)
        {
            var response = new ApiResponse<Products>();
            try
            {
                if (ModelState.IsValid)
                {
                    var Product = _mapper.Map<Products>(productDto);
                    var serviceResponse = await _auctionsService.CreateProduct(Product);

                    response.Result = serviceResponse.Result;
                    response.Message = serviceResponse.Message;
                    response.IsSuccess = serviceResponse.IsSuccess;

                    return response;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = string.Join("; ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));

                    return response;
                }
            } catch (Exception ex)
            {
                response.Message = ex.Message.ToString();
                return response;
            }
        }


        [HttpGet("userAuctions/{email}")]
        public async Task<ActionResult<ApiResponse<ICollection<Auctions>>>> GetUserAuctions(string email)
        {
            var response = new ApiResponse<ICollection<Auctions>>();
            try
            {
                var serviceResponse = await _auctionsService.GetUserAuctions(email);

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

        [HttpGet("UserparticipatedAuctions/{email}")]
        public async Task<ActionResult<ApiResponse<ICollection<BidsDto>>>> GetUserParticipatedAuctionBids(string email)
        {
            var response = new ApiResponse<ICollection<BidsDto>>();

            try
            {
                var serviceResponse = await _auctionsService.GetUserParticipatedBids(email);

                if (serviceResponse.IsSuccess && serviceResponse.Result != null)
                {
                    var UserBids = _mapper.Map<ICollection<BidsDto>>(serviceResponse.Result);
                    response.Result = UserBids;
                    response.Message = $"All Bids of User: {email} Retrived Successfully";
                    response.IsSuccess = true;
                }
                else
                {
                    response.Message = serviceResponse.Message;
                }

                return response;
            }
            catch(Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message.ToString()}";
                return response;
            }
        }

    }
}
