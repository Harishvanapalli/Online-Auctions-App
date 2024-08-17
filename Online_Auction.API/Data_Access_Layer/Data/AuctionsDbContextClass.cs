using Data_Access_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Data
{
    public class AuctionsDbContextClass : DbContext
    {
        public AuctionsDbContextClass(DbContextOptions<AuctionsDbContextClass> options) : base(options)
        {
            
        }

        public DbSet<Products> ProductsTable { get; set; }

        public DbSet<Auctions> AuctionsTable { get; set; }

        public DbSet<Users> UsersTable { get; set; }

        public DbSet<Bids> BidsTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auctions>().HasOne(a => a.Product).WithMany(p => p.Auctions).HasForeignKey(a => a.ProductID);

            modelBuilder.Entity<Bids>().HasOne(b => b.Auction).WithMany(a => a.Bids).HasForeignKey(b => b.AuctionID);

            modelBuilder.Entity<Users>().HasData(
                 new Users
                 {
                     UserID = 1,
                     UserName = "Harish",
                     EmailId = "harishvanapalli9@gmail.com",
                     Password = "Harish@123",
                     Role = "Administrator"
                 },
                 new Users
                 {
                     UserID = 2,
                     UserName = "Ravi",
                     EmailId = "ravivanapalli9@gmail.com",
                     Password = "Ravi@123",
                     Role = "Administrator"
                 },
                 new Users
                 {
                     UserID = 3,
                     UserName = "Dileep",
                     EmailId = "dileepthondupu8@gmail.com",
                     Password = "Dileep@123",
                     Role = "User"
                 },
                 new Users
                 {
                     UserID = 4,
                     UserName = "Mohan",
                     EmailId = "mohanuchula10@gmail.com",
                     Password = "Mohan@123",
                     Role = "User"
                 },
                 new Users
                 {
                     UserID = 5,
                     UserName = "Ramesh",
                     EmailId = "rameshupparapalli108@gmail.com",
                     Password = "Ramesh@123",
                     Role = "User"
                 },
                 new Users
                 {
                     UserID = 6,
                     UserName = "Naveen",
                     EmailId = "naveenbuddha9@gmail.com",
                     Password = "Naveen@123",
                     Role = "User"
                 }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
