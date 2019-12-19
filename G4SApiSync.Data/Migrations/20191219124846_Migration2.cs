using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AcadmicYear",
                schema: "g4s",
                table: "SyncResults",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearGroup",
                schema: "g4s",
                table: "SyncResults",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcadmicYear",
                schema: "g4s",
                table: "SyncResults");

            migrationBuilder.DropColumn(
                name: "YearGroup",
                schema: "g4s",
                table: "SyncResults");
        }
    }
}
