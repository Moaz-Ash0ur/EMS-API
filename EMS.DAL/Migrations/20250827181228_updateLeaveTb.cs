using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateLeaveTb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Leaves",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Leaves");
        }
    }
}
