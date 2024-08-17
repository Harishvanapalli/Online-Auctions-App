using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class Auctions
    {
        public int AuctionID { get; set; }

        public int ProductID { get; set; }

        public DateTime? AcutionStartedTime { get; set; } = DateTime.Now;

        public Boolean AuctionInProgress { get; set; } = true;

        public double CurrentBidValue { get; set; }

        public string? CurrentBidUser { get; set; } = null;

        public Boolean Sold { get; set; } = false;
    }
}
