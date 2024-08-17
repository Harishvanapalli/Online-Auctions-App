﻿// <auto-generated />
using System;
using Data_Access_Layer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    [DbContext(typeof(AuctionsDbContextClass))]
    partial class AuctionsDbContextClassModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Data_Access_Layer.Entities.Auctions", b =>
                {
                    b.Property<int>("AuctionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuctionID"));

                    b.Property<DateTime?>("AcutionStartedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("AuctionInProgress")
                        .HasColumnType("bit");

                    b.Property<string>("CurrentBidUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("CurrentBidValue")
                        .HasColumnType("float");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<bool>("Sold")
                        .HasColumnType("bit");

                    b.HasKey("AuctionID");

                    b.HasIndex("ProductID");

                    b.ToTable("AuctionsTable");
                });

            modelBuilder.Entity("Data_Access_Layer.Entities.Bids", b =>
                {
                    b.Property<int>("BidID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BidID"));

                    b.Property<int>("AuctionID")
                        .HasColumnType("int");

                    b.Property<double>("BidValue")
                        .HasColumnType("float");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BidID");

                    b.HasIndex("AuctionID");

                    b.ToTable("BidsTable");
                });

            modelBuilder.Entity("Data_Access_Layer.Entities.Products", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductID"));

                    b.Property<int>("AuctionDuration")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductCategory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ProductSold")
                        .HasColumnType("bit");

                    b.Property<double>("ReservedPrice")
                        .HasColumnType("float");

                    b.Property<double>("StartingPrice")
                        .HasColumnType("float");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductID");

                    b.ToTable("ProductsTable");
                });

            modelBuilder.Entity("Data_Access_Layer.Entities.Users", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Suspend")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("UsersTable");

                    b.HasData(
                        new
                        {
                            UserID = 1,
                            EmailId = "harishvanapalli9@gmail.com",
                            Password = "Harish@123",
                            Role = "Administrator",
                            Suspend = false,
                            UserName = "Harish"
                        },
                        new
                        {
                            UserID = 2,
                            EmailId = "ravivanapalli9@gmail.com",
                            Password = "Ravi@123",
                            Role = "Administrator",
                            Suspend = false,
                            UserName = "Ravi"
                        },
                        new
                        {
                            UserID = 3,
                            EmailId = "dileepthondupu8@gmail.com",
                            Password = "Dileep@123",
                            Role = "User",
                            Suspend = false,
                            UserName = "Dileep"
                        },
                        new
                        {
                            UserID = 4,
                            EmailId = "mohanuchula10@gmail.com",
                            Password = "Mohan@123",
                            Role = "User",
                            Suspend = false,
                            UserName = "Mohan"
                        },
                        new
                        {
                            UserID = 5,
                            EmailId = "rameshupparapalli108@gmail.com",
                            Password = "Ramesh@123",
                            Role = "User",
                            Suspend = false,
                            UserName = "Ramesh"
                        },
                        new
                        {
                            UserID = 6,
                            EmailId = "naveenbuddha9@gmail.com",
                            Password = "Naveen@123",
                            Role = "User",
                            Suspend = false,
                            UserName = "Naveen"
                        });
                });

            modelBuilder.Entity("Data_Access_Layer.Entities.Auctions", b =>
                {
                    b.HasOne("Data_Access_Layer.Entities.Products", "Product")
                        .WithMany("Auctions")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Data_Access_Layer.Entities.Bids", b =>
                {
                    b.HasOne("Data_Access_Layer.Entities.Auctions", "Auction")
                        .WithMany("Bids")
                        .HasForeignKey("AuctionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auction");
                });

            modelBuilder.Entity("Data_Access_Layer.Entities.Auctions", b =>
                {
                    b.Navigation("Bids");
                });

            modelBuilder.Entity("Data_Access_Layer.Entities.Products", b =>
                {
                    b.Navigation("Auctions");
                });
#pragma warning restore 612, 618
        }
    }
}
