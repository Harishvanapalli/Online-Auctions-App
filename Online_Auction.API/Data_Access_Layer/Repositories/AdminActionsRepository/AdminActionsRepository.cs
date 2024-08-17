using Azure;
using Data_Access_Layer.Data;
using Data_Access_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories.AdminActionsRepository
{
    public class AdminActionsRepository : IAdminActionsRepository
    {
        private readonly AuctionsDbContextClass _dbContext;

        public AdminActionsRepository(AuctionsDbContextClass _dbContext)
        {
            this._dbContext = _dbContext;
        }

        public async Task<ApiResponse<Auctions>> DeleteAuctionByID(int AuctionID)
        {
            var response = new ApiResponse<Auctions>();
            try
            {
                var Auction = await _dbContext.AuctionsTable.Include(a => a.Bids).FirstOrDefaultAsync(a => a.AuctionID == AuctionID);
                if(Auction == null)
                {
                    response.Result = null;
                    response.Message = $"No Auction found with ID: {AuctionID}";
                    response.IsSuccess = false;
                }
                else
                {
                    _dbContext.BidsTable.RemoveRange(Auction.Bids);
                    _dbContext.AuctionsTable.Remove(Auction);
                    await _dbContext.SaveChangesAsync();
                    response.Result = null;
                    response.Message = $"Auction with ID: {AuctionID} Deleted Sucessfully";
                    response.IsSuccess = true;
                }
                return response;
            }catch(Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message.ToString()}";
                return response;
            }
        }

        public async Task<ApiResponse<ICollection<Auctions>>> GetALlActiveAuctions()
        {
            var response = new ApiResponse<ICollection<Auctions>>();
            try
            {
                var Auctions = await _dbContext.AuctionsTable.Include(a => a.Bids).Include(a => a.Product).Where(a => a.AuctionInProgress == true).ToListAsync();

                response.Result = Auctions;
                response.Message = "All the Active Auctions Retrieved Successfully";
                response.IsSuccess = true;
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
                var Users = await _dbContext.UsersTable.Where(u => u.Role == "User").ToListAsync();

                response.Result = Users;
                response.IsSuccess = true;
                response.Message = "All Users Retrieved Successfully";

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
                var usersAuctions = await _dbContext.AuctionsTable.Include(a => a.Product).ToListAsync();

                foreach (var Auction in usersAuctions)
                {
                    await _dbContext.Entry(Auction).Collection(a => a.Bids).LoadAsync();
                }

                response.Result = usersAuctions;
                response.IsSuccess = true;
                response.Message = "Users Auctions Retrieved Successfully";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message.ToString()}";
                return response;
            }
        }

        public async Task<ApiResponse<ICollection<Bids>>> GetUsersparticipatedBids()
        {
            var response = new ApiResponse<ICollection<Bids>>();

            try
            {
                var userBids = await _dbContext.BidsTable.Include(b => b.Auction).ThenInclude(a => a.Product).ToListAsync();

                response.Result = userBids;
                response.IsSuccess = true;
                response.Message = "Users Bids Retrieved Successfully";

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
            try{
                _dbContext.UsersTable.Update(user);
                await _dbContext.SaveChangesAsync();

                response.Result = user;
                response.Message = $"User with Id: {user.UserID} updated Successfully";
                response.IsSuccess = true;

                return response;
            }catch(Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message.ToString()}";
                return response;
            }
        }
    }
}
