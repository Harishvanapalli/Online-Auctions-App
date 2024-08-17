using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories.AdminActionsRepository;
using Data_Access_Layer.Repositories.AuctionsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.AdminActionsService
{
    public class AdminActionsService : IAdminActionsService
    {
        private readonly IAdminActionsRepository _adminActionsRepository;

        public AdminActionsService(IAdminActionsRepository _adminActionsRepository)
        {
            this._adminActionsRepository = _adminActionsRepository;
        }
        public async Task<ApiResponse<Auctions>> DeleteAuctionByID(int AuctionID)
        {
            var response = new ApiResponse<Auctions>();
            try
            {
                var repositoryResponse = await _adminActionsRepository.DeleteAuctionByID(AuctionID);
                response.Result = repositoryResponse.Result;
                response.Message = repositoryResponse.Message;
                response.IsSuccess = repositoryResponse.IsSuccess;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message.ToString()}";
                return response;
            }
        }

        public async Task<ApiResponse<ICollection<Bids>>> GerUsersParticipatedAuctions()
        {
            var response = new ApiResponse<ICollection<Bids>>();
            try
            {
                var repositoryResponse = await _adminActionsRepository.GetUsersparticipatedBids();
                response.Result = repositoryResponse.Result;
                response.Message = repositoryResponse.Message;
                response.IsSuccess = repositoryResponse.IsSuccess;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message.ToString()}";
                return response;
            }
        }

        public async Task<ApiResponse<ICollection<Auctions>>> GetAllActiveAuctions()
        {
            var response = new ApiResponse<ICollection<Auctions>>();
            try
            {
                var repositoryResponse = await _adminActionsRepository.GetALlActiveAuctions();
                response.Result = repositoryResponse.Result;
                response.Message = repositoryResponse.Message;
                response.IsSuccess = repositoryResponse.IsSuccess;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message.ToString()}";
                return response;
            }

        }

        public async Task<ApiResponse<ICollection<Users>>> GetAllUsers()
        {
            var response = new ApiResponse<ICollection<Users>>();
            try
            {
                var repositoryResponse = await _adminActionsRepository.GetAllUsers();
                response.Result = repositoryResponse.Result;
                response.Message = repositoryResponse.Message;
                response.IsSuccess = repositoryResponse.IsSuccess;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message.ToString()}";
                return response;
            }
        }

        public async Task<ApiResponse<ICollection<Auctions>>> GetAuctionsStartedByUsers()
        {
            var response = new ApiResponse<ICollection<Auctions>>();
            try
            {
                var repositoryResponse = await _adminActionsRepository.GetAuctionsStartedByUsers();
                response.Result = repositoryResponse.Result;
                response.Message = repositoryResponse.Message;
                response.IsSuccess = repositoryResponse.IsSuccess;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message.ToString()}";
                return response;
            }
        }

        public async Task<ApiResponse<Users>> SuspendUserByID(Users user)
        {
            var response = new ApiResponse<Users>();
            try
            {
                var repositoryResponse = await _adminActionsRepository.SuspendUserByID(user);
                response.Result = repositoryResponse.Result;
                response.Message = repositoryResponse.Message;
                response.IsSuccess = repositoryResponse.IsSuccess;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message.ToString()}";
                return response;
            }
        }
    }
}
