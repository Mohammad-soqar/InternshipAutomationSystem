using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Internship.DataAccess.Migrations
{
    public partial class AddSubmitedFormsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "submittedApplicationForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    InternshipCoordinatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_submittedApplicationForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_submittedApplicationForms_Coordinators_InternshipCoordinatorId",
                        column: x => x.InternshipCoordinatorId,
                        principalTable: "Coordinators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_submittedApplicationForms_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_submittedApplicationForms_InternshipCoordinatorId",
                table: "submittedApplicationForms",
                column: "InternshipCoordinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_submittedApplicationForms_StudentId",
                table: "submittedApplicationForms",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "submittedApplicationForms");
        }
    }
}
