using System.ComponentModel.DataAnnotations;

namespace Online_Auction.API.Dto_s
{
    public class CreateAuctionDto
    {
        [Required(ErrorMessage = "ProductId is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "ProductId should be a positive Int")]
        public int ProductID { get; set; }


        [Required(ErrorMessage = "Current Bid Value is Required")]
        [Range(0, double.MaxValue, ErrorMessage = "Current Bid Value should be a positive value")]
        public double CurrentBidValue { get; set; }
    }
}
