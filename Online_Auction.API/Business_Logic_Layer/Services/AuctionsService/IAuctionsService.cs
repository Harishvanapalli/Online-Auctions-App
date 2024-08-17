using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.AuctionsService
{
    public interface IAuctionsService
    {
        Task<ApiResponse<Products>> CreateProduct(Products product);

        Task<ApiResponse<Auctions>> CreateAuction(Auctions Auction);

        Task<ApiResponse<ICollection<Auctions>>> GetUserAuctions(string email);

        Task<ApiResponse<ICollection<Auctions>>> GetAllActiveAuctions();

        Task<ApiResponse<Bids>> UserparticipationInAuction(Bids userBid);

        Task<ApiResponse<ICollection<Bids>>> GetUserParticipatedBids(string email);
    }
}
