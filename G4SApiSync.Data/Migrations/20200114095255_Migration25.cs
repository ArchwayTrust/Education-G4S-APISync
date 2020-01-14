using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration25 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriorAttainmentValues",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "PriorAttainmentTypes",
                schema: "g4s");

            migrationBuilder.CreateTable(
                name: "PriorAttainment",
                schema: "g4s",
                columns: table => new
                {
                    PriorAttainmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<string>(maxLength: 100, nullable: true),
                    AcademicYear = table.Column<string>(maxLength: 100, nullable: true),
                    Academy = table.Column<string>(maxLength: 100, nullable: true),
                    Code = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    Value = table.Column<string>(maxLength: 100, nullable: true),
                    ValueAcademicYear = table.Column<string>(maxLength: 100, nullable: true),
                    ValueDate = table.Column<DateTime>(type: "Date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriorAttainment", x => x.PriorAttainmentId);
                    table.ForeignKey(
                        name: "FK_PriorAttainment_Students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "g4s",
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriorAttainment_StudentId",
                schema: "g4s",
                table: "PriorAttainment",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriorAttainment",
                schema: "g4s");

            migrationBuilder.CreateTable(
                name: "PriorAttainmentTypes",
                schema: "g4s",
                columns: table => new
                {
                    PriorAttainmentTypeId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AcademicYear = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    Academy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriorAttainmentTypes", x => x.PriorAttainmentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "PriorAttainmentValues",
                schema: "g4s",
                columns: table => new
                {
                    PriorAttainmentValueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcademicYear = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Date = table.Column<DateTime>(type: "Date", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PriorAttainmentTypeId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StudentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriorAttainmentValues", x => x.PriorAttainmentValueId);
                    table.ForeignKey(
                        name: "FK_PriorAttainmentValues_PriorAttainmentTypes_PriorAttainmentTypeId",
                        column: x => x.PriorAttainmentTypeId,
                        principalSchema: "g4s",
                        principalTable: "PriorAttainmentTypes",
                        principalColumn: "PriorAttainmentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriorAttainmentValues_Students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "g4s",
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriorAttainmentValues_PriorAttainmentTypeId",
                schema: "g4s",
                table: "PriorAttainmentValues",
                column: "PriorAttainmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PriorAttainmentValues_StudentId",
                schema: "g4s",
                table: "PriorAttainmentValues",
                column: "StudentId");
        }
    }
}
