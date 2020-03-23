using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OtomatikMuhendis.Kutuphane.Web.Data.Migrations
{
    public partial class RawgImplemented : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RawgId",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RawgId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Items");
        }
    }
}
