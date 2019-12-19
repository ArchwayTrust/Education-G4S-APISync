using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttributes_EducationDetails_StudentId",
                schema: "g4s",
                table: "StudentAttributes");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttributes_EducationDetails_StudentId",
                schema: "g4s",
                table: "StudentAttributes",
                column: "StudentId",
                principalSchema: "g4s",
                principalTable: "EducationDetails",
                principalColumn: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttributes_EducationDetails_StudentId",
                schema: "g4s",
                table: "StudentAttributes");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttributes_EducationDetails_StudentId",
                schema: "g4s",
                table: "StudentAttributes",
                column: "StudentId",
                principalSchema: "g4s",
                principalTable: "EducationDetails",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
