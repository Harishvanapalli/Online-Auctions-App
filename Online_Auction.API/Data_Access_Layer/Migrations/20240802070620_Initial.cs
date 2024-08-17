using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductsTable",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartingPrice = table.Column<double>(type: "float", nullable: false),
                    AuctionDuration = table.Column<int>(type: "int", nullable: false),
                    ProductCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReservedPrice = table.Column<double>(type: "float", nullable: false),
                    ProductSold = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsTable", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "UsersTable",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersTable", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "AuctionsTable",
                columns: table => new
                {
                    AuctionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    AcutionStartedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuctionInProgress = table.Column<bool>(type: "bit", nullable: false),
                    CurrentBidValue = table.Column<double>(type: "float", nullable: false),
                    CurrentBidUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sold = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionsTable", x => x.AuctionID);
                    table.ForeignKey(
                        name: "FK_AuctionsTable_ProductsTable_ProductID",
                        column: x => x.ProductID,
                        principalTable: "ProductsTable",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BidsTable",
                columns: table => new
                {
                    BidID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuctionID = table.Column<int>(type: "int", nullable: false),
                    EmailID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BidValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidsTable", x => x.BidID);
                    table.ForeignKey(
                        name: "FK_BidsTable_AuctionsTable_AuctionID",
                        column: x => x.AuctionID,
                        principalTable: "AuctionsTable",
                        principalColumn: "AuctionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "UsersTable",
                columns: new[] { "UserID", "EmailId", "Password", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, "harishvanapalli9@gmail.com", "Harish@123", "Administrator", "Harish" },
                    { 2, "ravivanapalli9@gmail.com", "Ravi@123", "Administrator", "Ravi" },
                    { 3, "dileepthondupu8@gmail.com", "Dileep@123", "User", "Dileep" },
                    { 4, "mohanuchula10@gmail.com", "Mohan@123", "User", "Mohan" },
                    { 5, "rameshupparapalli108@gmail.com", "Ramesh@123", "User", "Ramesh" },
                    { 6, "naveenbuddha9@gmail.com", "Naveen@123", "User", "Naveen" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuctionsTable_ProductID",
                table: "AuctionsTable",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_BidsTable_AuctionID",
                table: "BidsTable",
                column: "AuctionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BidsTable");

            migrationBuilder.DropTable(
                name: "UsersTable");

            migrationBuilder.DropTable(
                name: "AuctionsTable");

            migrationBuilder.DropTable(
                name: "ProductsTable");
        }
    }
}
