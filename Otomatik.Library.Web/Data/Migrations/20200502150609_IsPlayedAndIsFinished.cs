using Microsoft.EntityFrameworkCore.Migrations;

namespace Otomatik.Library.Web.Data.Migrations
{
    public partial class IsPlayedAndIsFinished : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Items",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPlayed",
                table: "Items",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IsPlayed",
                table: "Items");
        }
    }
}
