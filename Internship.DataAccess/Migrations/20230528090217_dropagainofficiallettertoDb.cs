﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Internship.DataAccess.Migrations
{
    public partial class dropagainofficiallettertoDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_OfficialLetter_OfficialLetterId1",
                table: "Students");

            migrationBuilder.DropTable(
                name: "OfficialLetter");

            migrationBuilder.DropIndex(
                name: "IX_Students_OfficialLetterId1",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "OfficialLetterId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "OfficialLetterId1",
                table: "Students");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OfficialLetterId",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OfficialLetterId1",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OfficialLetter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternshipCoordinatorId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CompanyEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficialLetterPDF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiverName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficialLetter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfficialLetter_Coordinators_InternshipCoordinatorId",
                        column: x => x.InternshipCoordinatorId,
                        principalTable: "Coordinators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfficialLetter_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_OfficialLetterId1",
                table: "Students",
                column: "OfficialLetterId1");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialLetter_InternshipCoordinatorId",
                table: "OfficialLetter",
                column: "InternshipCoordinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficialLetter_StudentId",
                table: "OfficialLetter",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_OfficialLetter_OfficialLetterId1",
                table: "Students",
                column: "OfficialLetterId1",
                principalTable: "OfficialLetter",
                principalColumn: "Id");
        }
    }
}
