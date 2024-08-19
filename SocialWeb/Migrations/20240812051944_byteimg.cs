using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialWeb.Migrations
{
    /// <inheritdoc />
    public partial class byteimg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
               name: "TempImage",
               table: "Users",
               type: "varbinary(max)",
               nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "TempImage",
                table: "Posts",
                type: "varbinary(max)",
                nullable: true);

            // Step 2: Copy and convert the data
            migrationBuilder.Sql(
                @"UPDATE Users SET TempImage = CONVERT(varbinary(max), Image);
          UPDATE Posts SET TempImage = CONVERT(varbinary(max), image);");

            // Step 3: Drop the old column
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "image",
                table: "Posts");

            // Step 4: Rename the temporary column to the original name
            migrationBuilder.RenameColumn(
                name: "TempImage",
                table: "Users",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "TempImage",
                table: "Posts",
                newName: "image");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "GroupChatMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "image",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "GroupChatMessages",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
