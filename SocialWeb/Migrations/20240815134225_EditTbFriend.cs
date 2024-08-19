using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialWeb.Migrations
{
    /// <inheritdoc />
    public partial class EditTbFriend : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friend_Users_FriendId",
                table: "Friend");

            migrationBuilder.DropForeignKey(
                name: "FK_Friend_Users_UserId",
                table: "Friend");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Friend",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "FriendId",
                table: "Friend",
                newName: "UserId2");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Friend",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Friend_FriendId",
                table: "Friend",
                newName: "IX_Friend_UserId2");

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_Users_UserId1",
                table: "Friend",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_Users_UserId2",
                table: "Friend",
                column: "UserId2",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friend_Users_UserId1",
                table: "Friend");

            migrationBuilder.DropForeignKey(
                name: "FK_Friend_Users_UserId2",
                table: "Friend");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Friend",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "UserId2",
                table: "Friend",
                newName: "FriendId");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Friend",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Friend_UserId2",
                table: "Friend",
                newName: "IX_Friend_FriendId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_Users_FriendId",
                table: "Friend",
                column: "FriendId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_Users_UserId",
                table: "Friend",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
