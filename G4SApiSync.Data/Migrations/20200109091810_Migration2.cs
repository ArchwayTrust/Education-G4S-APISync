using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcademicYear",
                schema: "g4s",
                table: "Markslots");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AcademicYear",
                schema: "g4s",
                table: "Markslots",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);
        }
    }
}
