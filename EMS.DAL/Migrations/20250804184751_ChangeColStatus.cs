using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Leaves");

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "Leaves",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "Leaves");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Leaves",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
