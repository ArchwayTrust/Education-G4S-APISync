using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AttendanceFrom",
                schema: "sec",
                table: "AcademySecurity",
                type: "Date",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AttendanceTo",
                schema: "sec",
                table: "AcademySecurity",
                type: "Date",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GetAttendance",
                schema: "sec",
                table: "AcademySecurity",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttendanceFrom",
                schema: "sec",
                table: "AcademySecurity");

            migrationBuilder.DropColumn(
                name: "AttendanceTo",
                schema: "sec",
                table: "AcademySecurity");

            migrationBuilder.DropColumn(
                name: "GetAttendance",
                schema: "sec",
                table: "AcademySecurity");
        }
    }
}
