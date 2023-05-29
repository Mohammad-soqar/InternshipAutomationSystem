using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Internship.DataAccess.Migrations
{
    public partial class AddSubmitedFormssToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "submittedApplicationForms",
                newName: "PDFForm");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PDFForm",
                table: "submittedApplicationForms",
                newName: "ImageUrl");
        }
    }
}
