using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Data_Access_Layer.Entities
{
    public class Auctions
    {
        [Key]
        public int AuctionID { get; set; }

        [ForeignKey("Products")]
        public int ProductID { get; set; }

        public DateTime? AcutionStartedTime { get; set; } = DateTime.Now;

        public Boolean AuctionInProgress { get; set; } = true;

        public double CurrentBidValue { get; set; }

        public string? CurrentBidUser { get; set; } = null;

        public ICollection<Bids> Bids { get; set; } = new List<Bids>();

        public Boolean Sold { get; set; } = false;

        public Products Product { get; set; }
    }
}
