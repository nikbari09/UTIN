using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UTIN.migrations
{
    /// <inheritdoc />
    public partial class newdatabasefourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "payment_mode",
                table: "Details",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "transaction_id",
                table: "Details",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "payment_mode",
                table: "Details");

            migrationBuilder.DropColumn(
                name: "transaction_id",
                table: "Details");
        }
    }
}
