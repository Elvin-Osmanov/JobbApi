using Microsoft.EntityFrameworkCore.Migrations;

namespace JobbApi.Data.Migrations
{
    public partial class JobPropertyAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApplied",
                table: "Jobs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsBookmarked",
                table: "Jobs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApplied",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "IsBookmarked",
                table: "Jobs");
        }
    }
}
