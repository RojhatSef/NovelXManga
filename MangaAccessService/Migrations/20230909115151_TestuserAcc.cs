using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaAccessService.Migrations
{
    public partial class TestuserAcc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UserActivityTimer",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserActivityTimer",
                table: "AspNetUsers");
        }
    }
}