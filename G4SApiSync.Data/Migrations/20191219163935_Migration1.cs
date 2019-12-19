using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeValues_Students_StudentId",
                schema: "g4s",
                table: "AttributeValues");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeValues_Students_StudentId",
                schema: "g4s",
                table: "AttributeValues",
                column: "StudentId",
                principalSchema: "g4s",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeValues_Students_StudentId",
                schema: "g4s",
                table: "AttributeValues");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeValues_Students_StudentId",
                schema: "g4s",
                table: "AttributeValues",
                column: "StudentId",
                principalSchema: "g4s",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
