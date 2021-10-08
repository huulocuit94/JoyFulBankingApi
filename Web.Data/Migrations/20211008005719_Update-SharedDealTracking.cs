using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Data.Migrations
{
    public partial class UpdateSharedDealTracking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedDealTrackings_Groups_GroupId",
                table: "SharedDealTrackings");

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupId",
                table: "SharedDealTrackings",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_SharedDealTrackings_Groups_GroupId",
                table: "SharedDealTrackings",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedDealTrackings_Groups_GroupId",
                table: "SharedDealTrackings");

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupId",
                table: "SharedDealTrackings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SharedDealTrackings_Groups_GroupId",
                table: "SharedDealTrackings",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
