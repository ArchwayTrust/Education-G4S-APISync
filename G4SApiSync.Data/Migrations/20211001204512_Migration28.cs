using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration28 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SessionMinutesLates",
                schema: "g4s",
                table: "StudentSessionMarks",
                newName: "SessionMinutesLate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SessionMinutesLate",
                schema: "g4s",
                table: "StudentSessionMarks",
                newName: "SessionMinutesLates");
        }
    }
}
