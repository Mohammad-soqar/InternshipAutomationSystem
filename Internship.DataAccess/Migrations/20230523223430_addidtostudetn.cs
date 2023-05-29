using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Internship.DataAccess.Migrations
{
    public partial class addidtostudetn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_submittedApplicationForms_submittedApplicationFormId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_submittedApplicationFormId",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "submittedApplicationFormId",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "submittedApplicationFormId1",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_submittedApplicationFormId1",
                table: "Students",
                column: "submittedApplicationFormId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_submittedApplicationForms_submittedApplicationFormId1",
                table: "Students",
                column: "submittedApplicationFormId1",
                principalTable: "submittedApplicationForms",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_submittedApplicationForms_submittedApplicationFormId1",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_submittedApplicationFormId1",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "submittedApplicationFormId1",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "submittedApplicationFormId",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_submittedApplicationFormId",
                table: "Students",
                column: "submittedApplicationFormId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_submittedApplicationForms_submittedApplicationFormId",
                table: "Students",
                column: "submittedApplicationFormId",
                principalTable: "submittedApplicationForms",
                principalColumn: "Id");
        }
    }
}
