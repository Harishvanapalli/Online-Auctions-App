using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories.AuctionsRepository
{
    public interface IAuctionsRepository
    {
        Task<ApiResponse<Products>> CreateProduct(Products product);
        Task<ApiResponse<Auctions>> CreateAuction(Auctions Auction);

        Task<ApiResponse<ICollection<Auctions>>> GetAuctionsofUser(string email);

        Task<ApiResponse<ICollection<Auctions>>> GetAllActiveAuctions();

        Task<ApiResponse<Auctions>> GetAuctionById(int AuctionID);

        Task<ApiResponse<Auctions>> UpdateAuction(Auctions Auction);

        Task<ApiResponse<Bids>> CreateUserBid(Bids userBid);

        Task<ApiResponse<ICollection<Bids>>> GetUserParticipatedBids(string email);

        Task<ICollection<Auctions>> GetAllActiveAuctionsToUpdate();
    }
}
