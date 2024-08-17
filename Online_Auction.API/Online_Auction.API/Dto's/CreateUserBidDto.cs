using Online_Auction.API.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Online_Auction.API.Dto_s
{
    public class CreateUserBidDto
    {
        [Required(ErrorMessage = "AuctionID is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "AuctionID should be a positive Int")]
        public int AuctionID { get; set; }

        [Required(ErrorMessage = "User Email is Required")]
        [EmailAddress(ErrorMessage = "Should enter an valid email address")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Bidvalue is Required")]
        [Range(0, double.MaxValue, ErrorMessage = "Bid Value shluld be a postive value")]
        public double BidValue { get; set; }

    }
}
