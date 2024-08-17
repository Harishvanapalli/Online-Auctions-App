using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories.AuctionsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.AuctionsService
{
    public class AuctionsService : IAuctionsService
    {
        private readonly IAuctionsRepository _auctionsRepository;

        public AuctionsService(IAuctionsRepository _auctionsRepository)
        {
            this._auctionsRepository = _auctionsRepository;
        }
        public async Task<ApiResponse<Products>> CreateProduct(Products product)
        {
            var response = new ApiResponse<Products>();
            try
            {
                var repositoryResponse = await _auctionsRepository.CreateProduct(product);
                response.Result = repositoryResponse.Result;
                response.Message = repositoryResponse.Message;
                response.IsSuccess = repositoryResponse.IsSuccess;
                return response;
            }catch(Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message.ToString()}";
                return response;
            }
        }

        public async Task<ApiResponse<Auctions>> CreateAuction(Auctions Auction)
        {
            var response = new ApiResponse<Auctions>();
            try
            {
                var repositoryResponse = await _auctionsRepository.CreateAuction(Auction);
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

        public async Task<ApiResponse<ICollection<Auctions>>> GetUserAuctions(string email)
        {
            var response = new ApiResponse<ICollection<Auctions>>();
            try
            {
                var repositoryResponse = await _auctionsRepository.GetAuctionsofUser(email);
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
                var repositoryResponse = await _auctionsRepository.GetAllActiveAuctions();
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

        public async Task<ApiResponse<Bids>> UserparticipationInAuction(Bids userBid)
        {
            var response = new ApiResponse<Bids>();
            try
            {
                var repositoryResponse = await _auctionsRepository.GetAuctionById(userBid.AuctionID);
                if (!repositoryResponse.IsSuccess)
                {
                    response.Message = repositoryResponse.Message;
                    return response;
                }
                if(repositoryResponse.Result == null)
                {
                    response.Message = $"No Auction with Id: {userBid.AuctionID} Found!";
                    return response;
                }
                Auctions Auction = (Auctions)repositoryResponse.Result;

                if(Auction.CurrentBidValue >= userBid.BidValue)
                {
                    response.Message = $"UserBid value must be greater than CurrentBid Vlaue which is {Auction.CurrentBidValue}";
                    return response;
                }

                // Update Auction
                Auction.CurrentBidValue = userBid.BidValue;
                Auction.CurrentBidUser = userBid.UserEmail;

                var updateAuctionResponse = await _auctionsRepository.UpdateAuction(Auction);

                if (!updateAuctionResponse.IsSuccess)
                {
                    response.Message = updateAuctionResponse.Message;
                    return response;
                }

                var createUserBidresponse = await _auctionsRepository.CreateUserBid(userBid);

                    response.Result = createUserBidresponse.Result;
                    response.Message = createUserBidresponse.Message;
                    response.IsSuccess = createUserBidresponse.IsSuccess;

                return response;
            }catch(Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message.ToString()}";
                return response;
            }
        }

        public async Task<ApiResponse<ICollection<Bids>>> GetUserParticipatedBids(string email)
        {
            var response = new ApiResponse<ICollection<Bids>>();
            try
            {
                var repositoryResponse = await _auctionsRepository.GetUserParticipatedBids(email);
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
