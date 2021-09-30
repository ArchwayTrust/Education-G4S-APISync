using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BehClassifications",
                schema: "g4s",
                columns: table => new
                {
                    BehClassificationId = table.Column<int>(type: "int", nullable: false),
                    DataSet = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    Academy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BehClassifications", x => x.BehClassificationId);
                });

            migrationBuilder.CreateTable(
                name: "BehEvents",
                schema: "g4s",
                columns: table => new
                {
                    BehEventId = table.Column<int>(type: "int", nullable: false),
                    DataSet = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    Academy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    EventTypeId = table.Column<int>(type: "int", nullable: false),
                    EventDate = table.Column<DateTime>(type: "Date", nullable: false),
                    Closed = table.Column<bool>(type: "bit", nullable: false),
                    Cancelled = table.Column<bool>(type: "bit", nullable: false),
                    RoomName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    GroupName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SubjectCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    YearGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchoolNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByStaffId = table.Column<int>(type: "int", nullable: false),
                    ModifiedTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedByStaffId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BehEvents", x => x.BehEventId);
                });

            migrationBuilder.CreateTable(
                name: "BehEventTypes",
                schema: "g4s",
                columns: table => new
                {
                    BehEventTypeId = table.Column<int>(type: "int", nullable: false),
                    DataSet = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    Academy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    BehClassificationId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Alias = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Significance = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Prioritise = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BehEventTypes", x => x.BehEventTypeId);
                });

            migrationBuilder.CreateTable(
                name: "BehEventStudents",
                schema: "g4s",
                columns: table => new
                {
                    BehEventId = table.Column<int>(type: "int", nullable: false),
                    G4SStuId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DataSet = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    Academy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    EventBehEventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BehEventStudents", x => x.BehEventId);
                    table.ForeignKey(
                        name: "FK_BehEventStudents_BehEvents_EventBehEventId",
                        column: x => x.EventBehEventId,
                        principalSchema: "g4s",
                        principalTable: "BehEvents",
                        principalColumn: "BehEventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BehEventStudents_EventBehEventId",
                schema: "g4s",
                table: "BehEventStudents",
                column: "EventBehEventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BehClassifications",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "BehEventStudents",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "BehEventTypes",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "BehEvents",
                schema: "g4s");
        }
    }
}
