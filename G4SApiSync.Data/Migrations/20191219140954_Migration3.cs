using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcadmicYear",
                schema: "g4s",
                table: "SyncResults");

            migrationBuilder.AddColumn<string>(
                name: "AcademicYear",
                schema: "g4s",
                table: "SyncResults",
                maxLength: 4,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcademicYear",
                schema: "g4s",
                table: "SyncResults");

            migrationBuilder.AddColumn<string>(
                name: "AcadmicYear",
                schema: "g4s",
                table: "SyncResults",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);
        }
    }
}
