using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Internship.DataAccess.Migrations
{
    public partial class dropofficiallettertoDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfficialLetters_Coordinators_InternshipCoordinatorId",
                table: "OfficialLetters");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficialLetters_Students_StudentId",
                table: "OfficialLetters");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_OfficialLetters_OfficialLetterId1",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfficialLetters",
                table: "OfficialLetters");

            migrationBuilder.RenameTable(
                name: "OfficialLetters",
                newName: "OfficialLetter");

            migrationBuilder.RenameIndex(
                name: "IX_OfficialLetters_StudentId",
                table: "OfficialLetter",
                newName: "IX_OfficialLetter_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_OfficialLetters_InternshipCoordinatorId",
                table: "OfficialLetter",
                newName: "IX_OfficialLetter_InternshipCoordinatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfficialLetter",
                table: "OfficialLetter",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialLetter_Coordinators_InternshipCoordinatorId",
                table: "OfficialLetter",
                column: "InternshipCoordinatorId",
                principalTable: "Coordinators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialLetter_Students_StudentId",
                table: "OfficialLetter",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_OfficialLetter_OfficialLetterId1",
                table: "Students",
                column: "OfficialLetterId1",
                principalTable: "OfficialLetter",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfficialLetter_Coordinators_InternshipCoordinatorId",
                table: "OfficialLetter");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficialLetter_Students_StudentId",
                table: "OfficialLetter");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_OfficialLetter_OfficialLetterId1",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfficialLetter",
                table: "OfficialLetter");

            migrationBuilder.RenameTable(
                name: "OfficialLetter",
                newName: "OfficialLetters");

            migrationBuilder.RenameIndex(
                name: "IX_OfficialLetter_StudentId",
                table: "OfficialLetters",
                newName: "IX_OfficialLetters_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_OfficialLetter_InternshipCoordinatorId",
                table: "OfficialLetters",
                newName: "IX_OfficialLetters_InternshipCoordinatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfficialLetters",
                table: "OfficialLetters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialLetters_Coordinators_InternshipCoordinatorId",
                table: "OfficialLetters",
                column: "InternshipCoordinatorId",
                principalTable: "Coordinators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialLetters_Students_StudentId",
                table: "OfficialLetters",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_OfficialLetters_OfficialLetterId1",
                table: "Students",
                column: "OfficialLetterId1",
                principalTable: "OfficialLetters",
                principalColumn: "Id");
        }
    }
}
