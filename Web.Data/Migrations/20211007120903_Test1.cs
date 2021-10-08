using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Data.Migrations
{
    public partial class Test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompaignGroupMappings_Group_GroupId",
                table: "CompaignGroupMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_Group_Users_CreatedByUserId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_Group_Users_ModifiedByUserId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUserMappings_Group_GroupId",
                table: "GroupUserMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_SharedDealTrackings_Group_GroupId",
                table: "SharedDealTrackings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Group",
                table: "Group");

            migrationBuilder.RenameTable(
                name: "Group",
                newName: "Groups");

            migrationBuilder.RenameIndex(
                name: "IX_Group_ModifiedByUserId",
                table: "Groups",
                newName: "IX_Groups_ModifiedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Group_CreatedByUserId",
                table: "Groups",
                newName: "IX_Groups_CreatedByUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "Groups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompaignGroupMappings_Groups_GroupId",
                table: "CompaignGroupMappings",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_CreatedByUserId",
                table: "Groups",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_ModifiedByUserId",
                table: "Groups",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUserMappings_Groups_GroupId",
                table: "GroupUserMappings",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_SharedDealTrackings_Groups_GroupId",
                table: "SharedDealTrackings",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompaignGroupMappings_Groups_GroupId",
                table: "CompaignGroupMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_CreatedByUserId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Users_ModifiedByUserId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUserMappings_Groups_GroupId",
                table: "GroupUserMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_SharedDealTrackings_Groups_GroupId",
                table: "SharedDealTrackings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.RenameTable(
                name: "Groups",
                newName: "Group");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_ModifiedByUserId",
                table: "Group",
                newName: "IX_Group_ModifiedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_CreatedByUserId",
                table: "Group",
                newName: "IX_Group_CreatedByUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Group",
                table: "Group",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompaignGroupMappings_Group_GroupId",
                table: "CompaignGroupMappings",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Users_CreatedByUserId",
                table: "Group",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Users_ModifiedByUserId",
                table: "Group",
                column: "ModifiedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUserMappings_Group_GroupId",
                table: "GroupUserMappings",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_SharedDealTrackings_Group_GroupId",
                table: "SharedDealTrackings",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
