using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriorAttainmentType",
                schema: "g4s",
                columns: table => new
                {
                    PriorAttainmentTypeId = table.Column<string>(maxLength: 100, nullable: false),
                    AcademicYear = table.Column<string>(maxLength: 4, nullable: true),
                    Academy = table.Column<string>(maxLength: 10, nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriorAttainmentType", x => x.PriorAttainmentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "PriorAttainmentValue",
                schema: "g4s",
                columns: table => new
                {
                    PriorAttainmentValueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<string>(maxLength: 100, nullable: true),
                    PriorAttainmentTypeId = table.Column<string>(maxLength: 100, nullable: true),
                    Value = table.Column<string>(maxLength: 100, nullable: true),
                    AcademicYear = table.Column<string>(maxLength: 100, nullable: true),
                    Date = table.Column<DateTime>(type: "Date", nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriorAttainmentValue", x => x.PriorAttainmentValueId);
                    table.ForeignKey(
                        name: "FK_PriorAttainmentValue_PriorAttainmentType_PriorAttainmentTypeId",
                        column: x => x.PriorAttainmentTypeId,
                        principalSchema: "g4s",
                        principalTable: "PriorAttainmentType",
                        principalColumn: "PriorAttainmentTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriorAttainmentValue_Students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "g4s",
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriorAttainmentValue_PriorAttainmentTypeId",
                schema: "g4s",
                table: "PriorAttainmentValue",
                column: "PriorAttainmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PriorAttainmentValue_StudentId",
                schema: "g4s",
                table: "PriorAttainmentValue",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriorAttainmentValue",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "PriorAttainmentType",
                schema: "g4s");
        }
    }
}
