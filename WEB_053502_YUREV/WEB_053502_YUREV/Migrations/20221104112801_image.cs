using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEB_053502_YUREV.Migrations
{
    public partial class image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageMimeType",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageMimeType",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);
        }
    }
}
