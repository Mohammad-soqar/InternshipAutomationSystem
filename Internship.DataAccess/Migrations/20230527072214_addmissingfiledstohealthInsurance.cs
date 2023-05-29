using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Internship.DataAccess.Migrations
{
    public partial class addmissingfiledstohealthInsurance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthInsurance_CareerCenters_CareerCenter_UserId",
                table: "HealthInsurance");

        

            migrationBuilder.DropIndex(
                name: "IX_HealthInsurance_CareerCenter_UserId",
                table: "HealthInsurance");

            migrationBuilder.DropColumn(
                name: "CareerCenter_UserId",
                table: "HealthInsurance");

           

            migrationBuilder.AddColumn<string>(
                name: "HealthInsuranceId",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HealthInsuranceId1",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CareerCenterId",
                table: "HealthInsurance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HealthInsurancePDF",
                table: "HealthInsurance",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "HealthInsurance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Students_HealthInsuranceId1",
                table: "Students",
                column: "HealthInsuranceId1");

            migrationBuilder.CreateIndex(
                name: "IX_HealthInsurance_CareerCenterId",
                table: "HealthInsurance",
                column: "CareerCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthInsurance_StudentId",
                table: "HealthInsurance",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthInsurance_CareerCenters_CareerCenterId",
                table: "HealthInsurance",
                column: "CareerCenterId",
                principalTable: "CareerCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthInsurance_Students_StudentId",
                table: "HealthInsurance",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_HealthInsurance_HealthInsuranceId1",
                table: "Students",
                column: "HealthInsuranceId1",
                principalTable: "HealthInsurance",
                principalColumn: "Id");

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
                name: "FK_HealthInsurance_CareerCenters_CareerCenterId",
                table: "HealthInsurance");

           

            migrationBuilder.DropForeignKey(
                name: "FK_Students_HealthInsurance_HealthInsuranceId1",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_submittedApplicationForms_submittedApplicationFormId1",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_HealthInsuranceId1",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_HealthInsurance_CareerCenterId",
                table: "HealthInsurance");

            migrationBuilder.DropIndex(
                name: "IX_HealthInsurance_StudentId",
                table: "HealthInsurance");

            migrationBuilder.DropColumn(
                name: "HealthInsuranceId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "HealthInsuranceId1",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CareerCenterId",
                table: "HealthInsurance");

            migrationBuilder.DropColumn(
                name: "HealthInsurancePDF",
                table: "HealthInsurance");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "HealthInsurance");

           

            migrationBuilder.AddColumn<int>(
                name: "CareerCenter_UserId",
                table: "HealthInsurance",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HealthInsurance_CareerCenter_UserId",
                table: "HealthInsurance",
                column: "CareerCenter_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthInsurance_CareerCenters_CareerCenter_UserId",
                table: "HealthInsurance",
                column: "CareerCenter_UserId",
                principalTable: "CareerCenters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_submittedApplicationForms_submittedApplicationFormId1",
                table: "Students",
                column: "submittedApplicationFormId1",
                principalTable: "submittedApplicationForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
