using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Internship.DataAccess.Migrations
{
    public partial class addidtostudetsn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_submittedApplicationForms_submittedApplicationFormId1",
                table: "Students");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_submittedApplicationForms_submittedApplicationFormId1",
                table: "Students");

          
        }
    }
}
