using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.AdminActionsService
{
    public interface IAdminActionsService
    {
        Task<ApiResponse<ICollection<Bids>>> GerUsersParticipatedAuctions();

        Task<ApiResponse<ICollection<Auctions>>> GetAuctionsStartedByUsers();

        Task<ApiResponse<Auctions>> DeleteAuctionByID(int AuctionID);

        Task<ApiResponse<ICollection<Auctions>>> GetAllActiveAuctions();

        Task<ApiResponse<ICollection<Users>>> GetAllUsers();

        Task<ApiResponse<Users>> SuspendUserByID(Users user);
    }
}
