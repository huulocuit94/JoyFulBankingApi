using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Data.Migrations
{
    public partial class UpdateDealUserMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "DealUserMappings");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "DealUserMappings");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DealUserMappings");

            migrationBuilder.AddColumn<Guid>(
                name: "FromGroupId",
                table: "DealUserMappings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DealUserMappings_FromGroupId",
                table: "DealUserMappings",
                column: "FromGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_DealUserMappings_Groups_FromGroupId",
                table: "DealUserMappings",
                column: "FromGroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DealUserMappings_Groups_FromGroupId",
                table: "DealUserMappings");

            migrationBuilder.DropIndex(
                name: "IX_DealUserMappings_FromGroupId",
                table: "DealUserMappings");

            migrationBuilder.DropColumn(
                name: "FromGroupId",
                table: "DealUserMappings");

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "DealUserMappings",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "Score",
                table: "DealUserMappings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "DealUserMappings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
