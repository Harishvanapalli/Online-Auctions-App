using Data_Access_Layer.Data;
using Data_Access_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repositories.AuctionsRepository
{
    public class AuctionsRepository : IAuctionsRepository
    {

        private readonly AuctionsDbContextClass _dbContext;

        public AuctionsRepository(AuctionsDbContextClass _auctionContext)
        {
            this._dbContext = _auctionContext;
        }

        public async Task<ApiResponse<Auctions>> CreateAuction(Auctions Auction)
        {
            var response = new ApiResponse<Auctions>();
            try
            {
                _dbContext.AuctionsTable.Add(Auction);
                await _dbContext.SaveChangesAsync();
                response.Result = Auction;
                response.Message = "Auction Started Successfully";
                response.IsSuccess = true;

                return response;

            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message.ToString()}";
                return response;
            }
        }

        public async Task<ApiResponse<Products>> CreateProduct(Products product)
        {
            var response = new ApiResponse<Products>();
            try
            {
                _dbContext.ProductsTable.Add(product);
                await _dbContext.SaveChangesAsync();
                response.Result = product;
                response.Message = "Product Created Successfully";
                response.IsSuccess = true;

                return response;

            }catch(Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message.ToString()}";
                return response;
            }
        }

        public async Task<ApiResponse<ICollection<Auctions>>> GetAuctionsofUser(string email)
        {
            var response = new ApiResponse<ICollection<Auctions>>();
            try
            {
                var Auctions = await _dbContext.AuctionsTable.Include(a => a.Product).Where(a => a.Product.UserEmail == email).ToListAsync();
                foreach(var Auction in Auctions)
                {
                    await _dbContext.Entry(Auction).Collection(a => a.Bids).LoadAsync();
                }
                response.Result = Auctions;
                response.IsSuccess = true;
                response.Message = $"Auctions of user: {email} retrieved Successfully";

                return response;
            }catch(Exception ex)
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
                var ActiveAuctions = await _dbContext.AuctionsTable.Include(a => a.Product).Where(x => x.AuctionInProgress == true).ToListAsync();
                response.Result = ActiveAuctions;
                response.IsSuccess = true;
                response.Message = $"All Active Auctions retrieved Successfully";

                return response;
            }
            catch (Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message.ToString()}";
                return response;
            }
        }

        public async Task<ApiResponse<Auctions>> GetAuctionById(int AuctionID)
        {
            var response = new ApiResponse<Auctions>();
            try
            {
                var Auction = await _dbContext.AuctionsTable.FirstOrDefaultAsync(a => a.AuctionID == AuctionID);

                response.Result = Auction;
                response.IsSuccess = true;
                response.Message = $"Auction with Id: {AuctionID} retrieved successfully";

                return response;
                
            }catch(Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message.ToString()}";
                return response;
            } 
        }

        public async Task<ApiResponse<Auctions>> UpdateAuction(Auctions Auction)
        {
            var response = new ApiResponse<Auctions>();
            try
            {
                _dbContext.AuctionsTable.Update(Auction);
                await _dbContext.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = $"Auction with Id: {Auction.AuctionID} Updated Successfully";

                return response;
            }catch(Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message.ToString()}";
                return response;
            }
        }

        public async Task<ApiResponse<Bids>> CreateUserBid(Bids userBid)
        {
            var response = new ApiResponse<Bids>();
            try
            {
                _dbContext.BidsTable.Add(userBid);
                await _dbContext.SaveChangesAsync();

                response.Result = userBid;
                response.IsSuccess = true;
                response.Message = "User Bid Created Succesfully";

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
                var userBids = await _dbContext.BidsTable.Include(b => b.Auction).ThenInclude(a => a.Product).Where(b => b.UserEmail == email).ToListAsync();

                response.Result = userBids;
                response.IsSuccess = true;
                response.Message = "User Bids Retrieved Successfully";

                return response;
            }catch(Exception ex)
            {
                response.Message = $"An error occurred: {ex.Message.ToString()}";
                return response;
            }
        }

        public async Task<ICollection<Auctions>> GetAllActiveAuctionsToUpdate()
        {
            try
            {
                var ActiveAuctions = await _dbContext.AuctionsTable.Include(a => a.Product).Where(x => x.AuctionInProgress == true).ToListAsync();

                return ActiveAuctions;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
