using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Internship.DataAccess.Migrations
{
    public partial class addSavedInternshipToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InternshipOpportunityId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SavedInternships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternshipOpportunityId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedInternships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedInternships_InternshipOpportunities_InternshipOpportunityId",
                        column: x => x.InternshipOpportunityId,
                        principalTable: "InternshipOpportunities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SavedInternships_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_InternshipOpportunityId",
                table: "Students",
                column: "InternshipOpportunityId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedInternships_InternshipOpportunityId",
                table: "SavedInternships",
                column: "InternshipOpportunityId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedInternships_StudentId",
                table: "SavedInternships",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_InternshipOpportunities_InternshipOpportunityId",
                table: "Students",
                column: "InternshipOpportunityId",
                principalTable: "InternshipOpportunities",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_InternshipOpportunities_InternshipOpportunityId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "SavedInternships");

            migrationBuilder.DropIndex(
                name: "IX_Students_InternshipOpportunityId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "InternshipOpportunityId",
                table: "Students");
        }
    }
}
