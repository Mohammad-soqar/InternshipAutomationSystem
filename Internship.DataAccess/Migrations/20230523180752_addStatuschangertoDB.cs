using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Internship.DataAccess.Migrations
{
    public partial class addStatuschangertoDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ApplicationFormSubmitted",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationFormSubmitted",
                table: "Students");
        }
    }
}
