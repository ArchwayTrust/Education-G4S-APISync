using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarkslotMarks_Markslots_MarkslotId",
                schema: "g4s",
                table: "MarkslotMarks");

            migrationBuilder.DropForeignKey(
                name: "FK_MarkslotMarks_Students_StudentId",
                schema: "g4s",
                table: "MarkslotMarks");

            migrationBuilder.DropIndex(
                name: "IX_MarkslotMarks_MarkslotId",
                schema: "g4s",
                table: "MarkslotMarks");

            migrationBuilder.DropColumn(
                name: "AcademicYear",
                schema: "g4s",
                table: "MarkslotMarks");

            migrationBuilder.DropColumn(
                name: "MarkslotId",
                schema: "g4s",
                table: "MarkslotMarks");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                schema: "g4s",
                table: "MarkslotMarks");

            migrationBuilder.CreateIndex(
                name: "IX_MarkslotMarks_MockslotId",
                schema: "g4s",
                table: "MarkslotMarks",
                column: "MockslotId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarkslotMarks_Markslots_MockslotId",
                schema: "g4s",
                table: "MarkslotMarks",
                column: "MockslotId",
                principalSchema: "g4s",
                principalTable: "Markslots",
                principalColumn: "MarkslotId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarkslotMarks_Students_StudentId",
                schema: "g4s",
                table: "MarkslotMarks",
                column: "StudentId",
                principalSchema: "g4s",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarkslotMarks_Markslots_MockslotId",
                schema: "g4s",
                table: "MarkslotMarks");

            migrationBuilder.DropForeignKey(
                name: "FK_MarkslotMarks_Students_StudentId",
                schema: "g4s",
                table: "MarkslotMarks");

            migrationBuilder.DropIndex(
                name: "IX_MarkslotMarks_MockslotId",
                schema: "g4s",
                table: "MarkslotMarks");

            migrationBuilder.AddColumn<string>(
                name: "AcademicYear",
                schema: "g4s",
                table: "MarkslotMarks",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarkslotId",
                schema: "g4s",
                table: "MarkslotMarks",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubjectId",
                schema: "g4s",
                table: "MarkslotMarks",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MarkslotMarks_MarkslotId",
                schema: "g4s",
                table: "MarkslotMarks",
                column: "MarkslotId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarkslotMarks_Markslots_MarkslotId",
                schema: "g4s",
                table: "MarkslotMarks",
                column: "MarkslotId",
                principalSchema: "g4s",
                principalTable: "Markslots",
                principalColumn: "MarkslotId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MarkslotMarks_Students_StudentId",
                schema: "g4s",
                table: "MarkslotMarks",
                column: "StudentId",
                principalSchema: "g4s",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
