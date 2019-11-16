using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPCore.Blog.Domain.Data.Migrations
{
    public partial class FixedArticles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataTime",
                table: "Articles");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateChange",
                table: "Articles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateChange",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "DataTime",
                table: "Articles",
                nullable: true);
        }
    }
}
