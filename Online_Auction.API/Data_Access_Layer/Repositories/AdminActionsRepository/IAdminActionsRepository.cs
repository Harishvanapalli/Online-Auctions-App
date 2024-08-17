using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories.AdminActionsRepository
{
    public interface IAdminActionsRepository
    {
        Task<ApiResponse<ICollection<Bids>>> GetUsersparticipatedBids();

        Task<ApiResponse<ICollection<Auctions>>> GetAuctionsStartedByUsers();

        Task<ApiResponse<Auctions>> DeleteAuctionByID(int AuctionID);

        Task<ApiResponse<ICollection<Auctions>>> GetALlActiveAuctions();

        Task<ApiResponse<ICollection<Users>>> GetAllUsers();

        Task<ApiResponse<Users>> SuspendUserByID(Users user);
    }
}
