using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Entities
{
    public class Bids
    {
        [Key]
        public int BidID { get; set; }

        [ForeignKey("Auctions")]
        public int AuctionID { get; set; }

        public string UserEmail { get; set; }

        public double BidValue { get; set; }

        public Auctions Auction { get; set; }
 
    }
}
