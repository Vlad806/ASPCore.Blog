using Microsoft.EntityFrameworkCore.Migrations;

namespace ASPCore.Blog.Domain.Data.Migrations
{
    public partial class AddDataTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DataTime",
                table: "Articles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataTime",
                table: "Articles");
        }
    }
}
