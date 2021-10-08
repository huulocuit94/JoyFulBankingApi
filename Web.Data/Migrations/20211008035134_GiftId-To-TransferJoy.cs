using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Data.Migrations
{
    public partial class GiftIdToTransferJoy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GiftId",
                table: "TransferJoys",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TransferJoys_GiftId",
                table: "TransferJoys",
                column: "GiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferJoys_Gifts_GiftId",
                table: "TransferJoys",
                column: "GiftId",
                principalTable: "Gifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferJoys_Gifts_GiftId",
                table: "TransferJoys");

            migrationBuilder.DropIndex(
                name: "IX_TransferJoys_GiftId",
                table: "TransferJoys");

            migrationBuilder.DropColumn(
                name: "GiftId",
                table: "TransferJoys");
        }
    }
}
