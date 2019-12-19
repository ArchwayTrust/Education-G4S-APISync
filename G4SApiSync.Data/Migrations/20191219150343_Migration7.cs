using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeValues_Students_StudentId",
                schema: "g4s",
                table: "AttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_AttributeValues_StudentId",
                schema: "g4s",
                table: "AttributeValues");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AttributeValues_StudentId",
                schema: "g4s",
                table: "AttributeValues",
                column: "StudentId");

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
