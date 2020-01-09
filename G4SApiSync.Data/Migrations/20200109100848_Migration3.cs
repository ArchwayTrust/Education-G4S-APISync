using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marksheets_Subjects_SubjectId",
                schema: "g4s",
                table: "Marksheets");

            migrationBuilder.DropForeignKey(
                name: "FK_Markslots_Marksheets_MarksheetId",
                schema: "g4s",
                table: "Markslots");

            migrationBuilder.AddForeignKey(
                name: "FK_Marksheets_Subjects_SubjectId",
                schema: "g4s",
                table: "Marksheets",
                column: "SubjectId",
                principalSchema: "g4s",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Markslots_Marksheets_MarksheetId",
                schema: "g4s",
                table: "Markslots",
                column: "MarksheetId",
                principalSchema: "g4s",
                principalTable: "Marksheets",
                principalColumn: "MarksheetId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marksheets_Subjects_SubjectId",
                schema: "g4s",
                table: "Marksheets");

            migrationBuilder.DropForeignKey(
                name: "FK_Markslots_Marksheets_MarksheetId",
                schema: "g4s",
                table: "Markslots");

            migrationBuilder.AddForeignKey(
                name: "FK_Marksheets_Subjects_SubjectId",
                schema: "g4s",
                table: "Marksheets",
                column: "SubjectId",
                principalSchema: "g4s",
                principalTable: "Subjects",
                principalColumn: "SubjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Markslots_Marksheets_MarksheetId",
                schema: "g4s",
                table: "Markslots",
                column: "MarksheetId",
                principalSchema: "g4s",
                principalTable: "Marksheets",
                principalColumn: "MarksheetId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
