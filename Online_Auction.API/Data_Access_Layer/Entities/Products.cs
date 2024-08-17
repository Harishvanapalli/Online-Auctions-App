using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Data_Access_Layer.Entities
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }

        public string UserEmail { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public double StartingPrice { get; set; }

        public int AuctionDuration { get; set; }

        public string ProductCategory { get; set; }

        public double ReservedPrice { get; set; }

        public Boolean ProductSold { get; set; } = false;

        public ICollection<Auctions> Auctions { get; set; } = new List<Auctions>();

    }
}
