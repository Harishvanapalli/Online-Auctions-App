using Online_Auction.API.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Online_Auction.API.Dto_s
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Email Address is Required!")]
        [EmailAddress(ErrorMessage = "Should enter valid Email Address")]
        public string UserEmail { get; set; }


        [Required(ErrorMessage = "Product Name is Required!")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "Length of Product Name should be between 10 to 20")]
        public string ProductName { get; set; }


        [Required(ErrorMessage = "Product Description is Required!")]
        [StringLength(50, MinimumLength = 15, ErrorMessage = "Length of Product Description should be between 15 to 50")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Starting Price is Required!")]
        [Range(0, double.MaxValue, ErrorMessage = "Starting Price should be a positive value")]
        public double StartingPrice { get; set; }


        [Required(ErrorMessage = "Auction Duration is Required!")]
        [Range(1, int.MaxValue, ErrorMessage = "Auction Duration should be a positive value")]
        public int AuctionDuration { get; set; }


        [Required(ErrorMessage = "Product Category is Required!")]
        [CustomValidationAttributes("Fashion", "Automotive", "Furniture", "Electronics")]
        public string ProductCategory { get; set; }


        [Required(ErrorMessage = "Reserved Price is Required")]
        [Range(0, double.MaxValue, ErrorMessage = "Reserved Price should be a positive value")]
        [ReservedPriceValidation("StartingPrice")]
        public double ReservedPrice { get; set; }
    }
}
