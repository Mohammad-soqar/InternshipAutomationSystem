using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Internship.DataAccess.Migrations
{
    public partial class SetDefaultStatusForStudesnt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "submittedApplicationFormId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Students_submittedApplicationFormId",
                table: "Students",
                column: "submittedApplicationFormId");

            migrationBuilder.AddForeignKey(
       name: "FK_Students_submittedApplicationForms_submittedApplicationFormId",
       table: "Students",
       column: "submittedApplicationFormId",
       principalTable: "submittedApplicationForms",
       principalColumn: "Id",
       onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_submittedApplicationForms_submittedApplicationFormId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_submittedApplicationFormId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "submittedApplicationFormId",
                table: "Students");
        }
    }
}
