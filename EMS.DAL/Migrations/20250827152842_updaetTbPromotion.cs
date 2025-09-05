using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updaetTbPromotion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Promotions",
                newName: "NewTitle");

            migrationBuilder.RenameColumn(
                name: "DatePromoted",
                table: "Promotions",
                newName: "EffectiveDate");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Promotions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Promotions");

            migrationBuilder.RenameColumn(
                name: "NewTitle",
                table: "Promotions",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "EffectiveDate",
                table: "Promotions",
                newName: "DatePromoted");
        }
    }
}
