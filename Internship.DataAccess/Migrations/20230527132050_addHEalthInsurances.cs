using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Internship.DataAccess.Migrations
{
    public partial class addHEalthInsurances : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthInsurance_CareerCenters_CareerCenterId",
                table: "HealthInsurance");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthInsurance_Students_StudentId",
                table: "HealthInsurance");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_HealthInsurance_HealthInsuranceId1",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HealthInsurance",
                table: "HealthInsurance");

            migrationBuilder.RenameTable(
                name: "HealthInsurance",
                newName: "HealthInsurances");

            migrationBuilder.RenameIndex(
                name: "IX_HealthInsurance_StudentId",
                table: "HealthInsurances",
                newName: "IX_HealthInsurances_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_HealthInsurance_CareerCenterId",
                table: "HealthInsurances",
                newName: "IX_HealthInsurances_CareerCenterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HealthInsurances",
                table: "HealthInsurances",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthInsurances_CareerCenters_CareerCenterId",
                table: "HealthInsurances",
                column: "CareerCenterId",
                principalTable: "CareerCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthInsurances_Students_StudentId",
                table: "HealthInsurances",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_HealthInsurances_HealthInsuranceId1",
                table: "Students",
                column: "HealthInsuranceId1",
                principalTable: "HealthInsurances",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthInsurances_CareerCenters_CareerCenterId",
                table: "HealthInsurances");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthInsurances_Students_StudentId",
                table: "HealthInsurances");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_HealthInsurances_HealthInsuranceId1",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HealthInsurances",
                table: "HealthInsurances");

            migrationBuilder.RenameTable(
                name: "HealthInsurances",
                newName: "HealthInsurance");

            migrationBuilder.RenameIndex(
                name: "IX_HealthInsurances_StudentId",
                table: "HealthInsurance",
                newName: "IX_HealthInsurance_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_HealthInsurances_CareerCenterId",
                table: "HealthInsurance",
                newName: "IX_HealthInsurance_CareerCenterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HealthInsurance",
                table: "HealthInsurance",
                column: "Id");

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
        }
    }
}
