using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Internship.DataAccess.Migrations
{
    public partial class addnull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_submittedApplicationForms_submittedApplicationFormId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "submittedApplicationFormId",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_submittedApplicationForms_submittedApplicationFormId",
                table: "Students",
                column: "submittedApplicationFormId",
                principalTable: "submittedApplicationForms",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_submittedApplicationForms_submittedApplicationFormId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "submittedApplicationFormId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_submittedApplicationForms_submittedApplicationFormId",
                table: "Students",
                column: "submittedApplicationFormId",
                principalTable: "submittedApplicationForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
