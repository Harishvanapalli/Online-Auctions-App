using System.ComponentModel.DataAnnotations;

namespace Online_Auction.API.Dto_s
{
    public class ProductsDto
    {
        public int ProductID { get; set; }

        public string UserEmail { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public double StartingPrice { get; set; }

        public int AuctionDuration { get; set; }

        public string ProductCategory { get; set; }

        public double ReservedPrice { get; set; }

        public Boolean ProductSold { get; set; }

    }
}
