using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaAccessService.Migrations
{
    public partial class Test33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastReadDate",
                table: "mangaModels",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastReadDate",
                table: "mangaModels");
        }
    }
}
