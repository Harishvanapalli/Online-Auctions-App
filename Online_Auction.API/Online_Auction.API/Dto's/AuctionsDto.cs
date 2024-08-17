using Data_Access_Layer.Entities;

namespace Online_Auction.API.Dto_s
{
    public class AuctionsDto
    {
        public int AuctionID { get; set; }

        public int ProductID { get; set; }

        public DateTime? AcutionStartedTime { get; set; } = DateTime.Now;

        public Boolean AuctionInProgress { get; set; } = true;

        public double CurrentBidValue { get; set; }

        public string? CurrentBidUser { get; set; } = null;

        public ProductsDto Product { get; set; }
    }
}
