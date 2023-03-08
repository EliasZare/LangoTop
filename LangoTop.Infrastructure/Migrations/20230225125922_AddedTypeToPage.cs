using Microsoft.EntityFrameworkCore.Migrations;

namespace LangoTop.Infrastructure.Migrations
{
    public partial class AddedTypeToPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Pages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Pages");
        }
    }
}
