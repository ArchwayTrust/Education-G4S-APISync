using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarksheetGrades_Marksheets_MarksheetId1",
                schema: "g4s",
                table: "MarksheetGrades");

            migrationBuilder.DropIndex(
                name: "IX_MarksheetGrades_MarksheetId1",
                schema: "g4s",
                table: "MarksheetGrades");

            migrationBuilder.DropColumn(
                name: "Mark",
                schema: "g4s",
                table: "MarksheetGrades");

            migrationBuilder.DropColumn(
                name: "MarksheetId1",
                schema: "g4s",
                table: "MarksheetGrades");

            migrationBuilder.AlterColumn<string>(
                name: "MarksheetId",
                schema: "g4s",
                table: "MarksheetGrades",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_MarksheetGrades_MarksheetId",
                schema: "g4s",
                table: "MarksheetGrades",
                column: "MarksheetId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarksheetGrades_Marksheets_MarksheetId",
                schema: "g4s",
                table: "MarksheetGrades",
                column: "MarksheetId",
                principalSchema: "g4s",
                principalTable: "Marksheets",
                principalColumn: "MarksheetId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarksheetGrades_Marksheets_MarksheetId",
                schema: "g4s",
                table: "MarksheetGrades");

            migrationBuilder.DropIndex(
                name: "IX_MarksheetGrades_MarksheetId",
                schema: "g4s",
                table: "MarksheetGrades");

            migrationBuilder.AlterColumn<int>(
                name: "MarksheetId",
                schema: "g4s",
                table: "MarksheetGrades",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Mark",
                schema: "g4s",
                table: "MarksheetGrades",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "MarksheetId1",
                schema: "g4s",
                table: "MarksheetGrades",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MarksheetGrades_MarksheetId1",
                schema: "g4s",
                table: "MarksheetGrades",
                column: "MarksheetId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MarksheetGrades_Marksheets_MarksheetId1",
                schema: "g4s",
                table: "MarksheetGrades",
                column: "MarksheetId1",
                principalSchema: "g4s",
                principalTable: "Marksheets",
                principalColumn: "MarksheetId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
