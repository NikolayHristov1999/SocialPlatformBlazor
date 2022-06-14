using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialPlatformBlazor.Server.Data.Migrations
{
    public partial class ImagePaths : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BackgroundImagePath",
                table: "Pages",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainImagePath",
                table: "Pages",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BackgroundImagePath",
                table: "AspNetUsers",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainImagePath",
                table: "AspNetUsers",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundImagePath",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "MainImagePath",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "BackgroundImagePath",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MainImagePath",
                table: "AspNetUsers");
        }
    }
}
