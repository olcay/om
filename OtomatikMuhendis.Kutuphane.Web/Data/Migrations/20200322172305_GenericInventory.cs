using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OtomatikMuhendis.Kutuphane.Web.Data.Migrations
{
    public partial class GenericInventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Books_BookId",
                table: "Notifications");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Notifications",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_BookId",
                table: "Notifications",
                newName: "IX_Notifications_ItemId");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Notifications",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedById = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ShelfId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_Shelves_ShelfId",
                        column: x => x.ShelfId,
                        principalTable: "Shelves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemBookDetails",
                columns: table => new
                {
                    ItemId = table.Column<int>(nullable: false),
                    BookDetailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemBookDetails", x => new { x.ItemId, x.BookDetailId });
                    table.ForeignKey(
                        name: "FK_ItemBookDetails_BookDetails_BookDetailId",
                        column: x => x.BookDetailId,
                        principalTable: "BookDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemBookDetails_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CreatedById",
                table: "Notifications",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ItemBookDetails_BookDetailId",
                table: "ItemBookDetails",
                column: "BookDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CreatedById",
                table: "Items",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ShelfId",
                table: "Items",
                column: "ShelfId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_CreatedById",
                table: "Notifications",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Items_ItemId",
                table: "Notifications",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_CreatedById",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Items_ItemId",
                table: "Notifications");

            migrationBuilder.DropTable(
                name: "ItemBookDetails");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_CreatedById",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Notifications",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_ItemId",
                table: "Notifications",
                newName: "IX_Notifications_BookId");

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BookDetailId = table.Column<int>(nullable: true),
                    CreatedById = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ShelfId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_BookDetails_BookDetailId",
                        column: x => x.BookDetailId,
                        principalTable: "BookDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Books_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Shelves_ShelfId",
                        column: x => x.ShelfId,
                        principalTable: "Shelves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookDetailId",
                table: "Books",
                column: "BookDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CreatedById",
                table: "Books",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ShelfId",
                table: "Books",
                column: "ShelfId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Books_BookId",
                table: "Notifications",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
