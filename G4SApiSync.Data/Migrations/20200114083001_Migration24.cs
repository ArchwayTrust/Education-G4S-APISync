using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriorAttainmentValue_PriorAttainmentType_PriorAttainmentTypeId",
                schema: "g4s",
                table: "PriorAttainmentValue");

            migrationBuilder.DropForeignKey(
                name: "FK_PriorAttainmentValue_Students_StudentId",
                schema: "g4s",
                table: "PriorAttainmentValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriorAttainmentValue",
                schema: "g4s",
                table: "PriorAttainmentValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriorAttainmentType",
                schema: "g4s",
                table: "PriorAttainmentType");

            migrationBuilder.RenameTable(
                name: "PriorAttainmentValue",
                schema: "g4s",
                newName: "PriorAttainmentValues",
                newSchema: "g4s");

            migrationBuilder.RenameTable(
                name: "PriorAttainmentType",
                schema: "g4s",
                newName: "PriorAttainmentTypes",
                newSchema: "g4s");

            migrationBuilder.RenameIndex(
                name: "IX_PriorAttainmentValue_StudentId",
                schema: "g4s",
                table: "PriorAttainmentValues",
                newName: "IX_PriorAttainmentValues_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_PriorAttainmentValue_PriorAttainmentTypeId",
                schema: "g4s",
                table: "PriorAttainmentValues",
                newName: "IX_PriorAttainmentValues_PriorAttainmentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriorAttainmentValues",
                schema: "g4s",
                table: "PriorAttainmentValues",
                column: "PriorAttainmentValueId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriorAttainmentTypes",
                schema: "g4s",
                table: "PriorAttainmentTypes",
                column: "PriorAttainmentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PriorAttainmentValues_PriorAttainmentTypes_PriorAttainmentTypeId",
                schema: "g4s",
                table: "PriorAttainmentValues",
                column: "PriorAttainmentTypeId",
                principalSchema: "g4s",
                principalTable: "PriorAttainmentTypes",
                principalColumn: "PriorAttainmentTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PriorAttainmentValues_Students_StudentId",
                schema: "g4s",
                table: "PriorAttainmentValues",
                column: "StudentId",
                principalSchema: "g4s",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriorAttainmentValues_PriorAttainmentTypes_PriorAttainmentTypeId",
                schema: "g4s",
                table: "PriorAttainmentValues");

            migrationBuilder.DropForeignKey(
                name: "FK_PriorAttainmentValues_Students_StudentId",
                schema: "g4s",
                table: "PriorAttainmentValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriorAttainmentValues",
                schema: "g4s",
                table: "PriorAttainmentValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PriorAttainmentTypes",
                schema: "g4s",
                table: "PriorAttainmentTypes");

            migrationBuilder.RenameTable(
                name: "PriorAttainmentValues",
                schema: "g4s",
                newName: "PriorAttainmentValue",
                newSchema: "g4s");

            migrationBuilder.RenameTable(
                name: "PriorAttainmentTypes",
                schema: "g4s",
                newName: "PriorAttainmentType",
                newSchema: "g4s");

            migrationBuilder.RenameIndex(
                name: "IX_PriorAttainmentValues_StudentId",
                schema: "g4s",
                table: "PriorAttainmentValue",
                newName: "IX_PriorAttainmentValue_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_PriorAttainmentValues_PriorAttainmentTypeId",
                schema: "g4s",
                table: "PriorAttainmentValue",
                newName: "IX_PriorAttainmentValue_PriorAttainmentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriorAttainmentValue",
                schema: "g4s",
                table: "PriorAttainmentValue",
                column: "PriorAttainmentValueId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriorAttainmentType",
                schema: "g4s",
                table: "PriorAttainmentType",
                column: "PriorAttainmentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PriorAttainmentValue_PriorAttainmentType_PriorAttainmentTypeId",
                schema: "g4s",
                table: "PriorAttainmentValue",
                column: "PriorAttainmentTypeId",
                principalSchema: "g4s",
                principalTable: "PriorAttainmentType",
                principalColumn: "PriorAttainmentTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PriorAttainmentValue_Students_StudentId",
                schema: "g4s",
                table: "PriorAttainmentValue",
                column: "StudentId",
                principalSchema: "g4s",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
