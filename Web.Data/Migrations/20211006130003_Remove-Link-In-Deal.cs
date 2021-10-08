using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Data.Migrations
{
    public partial class RemoveLinkInDeal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "Deals");

            migrationBuilder.RenameColumn(
                name: "SourceLink",
                table: "Deals",
                newName: "Code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Deals",
                newName: "SourceLink");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Deals",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
