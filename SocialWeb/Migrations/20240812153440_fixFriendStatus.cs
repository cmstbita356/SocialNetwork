using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialWeb.Migrations
{
    /// <inheritdoc />
    public partial class fixFriendStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "statis",
                table: "Friend",
                newName: "status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "Friend",
                newName: "statis");
        }
    }
}
