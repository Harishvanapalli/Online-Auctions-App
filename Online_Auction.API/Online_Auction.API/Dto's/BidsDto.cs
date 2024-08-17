using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Online_Auction.API.Dto_s
{
    public class BidsDto
    {
        public int BidID { get; set; }

        public int AuctionID { get; set; }

        public string UserEmail { get; set; }

        public double BidValue { get; set; }

        public AuctionsDto Auction { get; set; }

    }
}
