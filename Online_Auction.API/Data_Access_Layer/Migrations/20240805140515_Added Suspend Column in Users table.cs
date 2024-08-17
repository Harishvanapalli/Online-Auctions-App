using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class AddedSuspendColumninUserstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Suspend",
                table: "UsersTable",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "UsersTable",
                keyColumn: "UserID",
                keyValue: 1,
                column: "Suspend",
                value: false);

            migrationBuilder.UpdateData(
                table: "UsersTable",
                keyColumn: "UserID",
                keyValue: 2,
                column: "Suspend",
                value: false);

            migrationBuilder.UpdateData(
                table: "UsersTable",
                keyColumn: "UserID",
                keyValue: 3,
                column: "Suspend",
                value: false);

            migrationBuilder.UpdateData(
                table: "UsersTable",
                keyColumn: "UserID",
                keyValue: 4,
                column: "Suspend",
                value: false);

            migrationBuilder.UpdateData(
                table: "UsersTable",
                keyColumn: "UserID",
                keyValue: 5,
                column: "Suspend",
                value: false);

            migrationBuilder.UpdateData(
                table: "UsersTable",
                keyColumn: "UserID",
                keyValue: 6,
                column: "Suspend",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Suspend",
                table: "UsersTable");
        }
    }
}
