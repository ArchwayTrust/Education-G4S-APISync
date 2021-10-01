using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration27 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SessionMinutesLates",
                schema: "g4s",
                table: "StudentSessionMarks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSessionMarks_SessionAliasId",
                schema: "g4s",
                table: "StudentSessionMarks",
                column: "SessionAliasId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSessionMarks_SessionMarkId",
                schema: "g4s",
                table: "StudentSessionMarks",
                column: "SessionMarkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentSessionMarks_SessionAliasId",
                schema: "g4s",
                table: "StudentSessionMarks");

            migrationBuilder.DropIndex(
                name: "IX_StudentSessionMarks_SessionMarkId",
                schema: "g4s",
                table: "StudentSessionMarks");

            migrationBuilder.AlterColumn<int>(
                name: "SessionMinutesLates",
                schema: "g4s",
                table: "StudentSessionMarks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
