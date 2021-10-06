using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Data.Migrations
{
    public partial class UpdateDeal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Deals_DealId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_DealId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "DealId",
                table: "Tags");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Deals",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Deals_CategoryId",
                table: "Deals",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deals_Categories_CategoryId",
                table: "Deals",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deals_Categories_CategoryId",
                table: "Deals");

            migrationBuilder.DropIndex(
                name: "IX_Deals_CategoryId",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Deals");

            migrationBuilder.AddColumn<Guid>(
                name: "DealId",
                table: "Tags",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_DealId",
                table: "Tags",
                column: "DealId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Deals_DealId",
                table: "Tags",
                column: "DealId",
                principalTable: "Deals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
