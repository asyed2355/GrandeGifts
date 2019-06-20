using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrandeGifts.Migrations
{
    public partial class AddingOrder_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    StreetAddress = table.Column<string>(nullable: true),
                    Suburb = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Postcode = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblLineItems_OrderId",
                table: "TblLineItems",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblLineItems_Order_OrderId",
                table: "TblLineItems",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblLineItems_Order_OrderId",
                table: "TblLineItems");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropIndex(
                name: "IX_TblLineItems_OrderId",
                table: "TblLineItems");
        }
    }
}
