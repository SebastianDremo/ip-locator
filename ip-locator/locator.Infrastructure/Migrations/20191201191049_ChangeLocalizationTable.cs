using Microsoft.EntityFrameworkCore.Migrations;

namespace locator.Infrastructure.Migrations
{
    public partial class ChangeLocalizationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Region",
                table: "Localizations");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Localizations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Localizations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Localizations");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Localizations");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "Localizations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
