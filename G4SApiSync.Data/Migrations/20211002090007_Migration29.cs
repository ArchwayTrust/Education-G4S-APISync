using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration29 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GetAttendance",
                schema: "sec",
                table: "AcademySecurity",
                newName: "GetSessionAttendance");

            migrationBuilder.AddColumn<bool>(
                name: "GetLessonAttendance",
                schema: "sec",
                table: "AcademySecurity",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GetLessonAttendance",
                schema: "sec",
                table: "AcademySecurity");

            migrationBuilder.RenameColumn(
                name: "GetSessionAttendance",
                schema: "sec",
                table: "AcademySecurity",
                newName: "GetAttendance");
        }
    }
}
