using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrandeGifts.Migrations
{
    public partial class AddingOrderAndLineItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblHampers_TblCategories_CategoryId",
                table: "TblHampers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TblCategories",
                table: "TblCategories");

            migrationBuilder.RenameTable(
                name: "TblCategories",
                newName: "Category");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "CategoryId");

            migrationBuilder.CreateTable(
                name: "TblLineItems",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(nullable: false),
                    HamperId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblLineItems", x => new { x.HamperId, x.OrderId });
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TblHampers_Category_CategoryId",
                table: "TblHampers",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblHampers_Category_CategoryId",
                table: "TblHampers");

            migrationBuilder.DropTable(
                name: "TblLineItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "TblCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TblCategories",
                table: "TblCategories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblHampers_TblCategories_CategoryId",
                table: "TblHampers",
                column: "CategoryId",
                principalTable: "TblCategories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
