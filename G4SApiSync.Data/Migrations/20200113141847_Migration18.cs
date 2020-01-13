using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationDetails_Students_StudentId",
                schema: "g4s",
                table: "EducationDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationDetails_Students_StudentId",
                schema: "g4s",
                table: "EducationDetails",
                column: "StudentId",
                principalSchema: "g4s",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationDetails_Students_StudentId",
                schema: "g4s",
                table: "EducationDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationDetails_Students_StudentId",
                schema: "g4s",
                table: "EducationDetails",
                column: "StudentId",
                principalSchema: "g4s",
                principalTable: "Students",
                principalColumn: "StudentId");
        }
    }
}
