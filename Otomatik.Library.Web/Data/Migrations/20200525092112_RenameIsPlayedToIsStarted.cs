using Microsoft.EntityFrameworkCore.Migrations;

namespace Otomatik.Library.Web.Data.Migrations
{
    public partial class RenameIsPlayedToIsStarted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPlayed",
                table: "Items");

            migrationBuilder.AddColumn<bool>(
                name: "IsStarted",
                table: "Items",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStarted",
                table: "Items");

            migrationBuilder.AddColumn<bool>(
                name: "IsPlayed",
                table: "Items",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
