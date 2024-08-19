using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialWeb.Migrations
{
    /// <inheritdoc />
    public partial class fixNameImg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "image",
                table: "Posts",
                newName: "Image");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Posts",
                newName: "image");
        }
    }
}
