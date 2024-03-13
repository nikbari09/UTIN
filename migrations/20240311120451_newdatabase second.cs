using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UTIN.migrations
{
    /// <inheritdoc />
    public partial class newdatabasesecond : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "count",
                table: "Details",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "count",
                table: "Details");
        }
    }
}
