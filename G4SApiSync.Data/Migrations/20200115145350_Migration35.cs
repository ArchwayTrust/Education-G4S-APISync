using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration35 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GradeTypes",
                schema: "g4s",
                columns: table => new
                {
                    GradeTypeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeTypes", x => x.GradeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "GradeNames",
                schema: "g4s",
                columns: table => new
                {
                    GradeNameId = table.Column<string>(maxLength: 100, nullable: false),
                    GradeTypeId = table.Column<int>(nullable: false),
                    AcademicYear = table.Column<string>(maxLength: 100, nullable: true),
                    Academy = table.Column<string>(maxLength: 100, nullable: true),
                    NCYear = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    ShortName = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    PreferredProgressGrade = table.Column<bool>(nullable: false),
                    PreferredTargetGrade = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeNames", x => x.GradeNameId);
                    table.ForeignKey(
                        name: "FK_GradeNames_GradeTypes_GradeTypeId",
                        column: x => x.GradeTypeId,
                        principalSchema: "g4s",
                        principalTable: "GradeTypes",
                        principalColumn: "GradeTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "g4s",
                table: "GradeTypes",
                columns: new[] { "GradeTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "External target" },
                    { 2, "Teacher target" },
                    { 3, "Combined target" },
                    { 4, "Current" },
                    { 5, "Project" },
                    { 6, "Actual" },
                    { 7, "Honest" },
                    { 8, "Aspirational" },
                    { 9, "Additional target" },
                    { 10, "Baseline grade" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GradeNames_GradeTypeId",
                schema: "g4s",
                table: "GradeNames",
                column: "GradeTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GradeNames",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "GradeTypes",
                schema: "g4s");
        }
    }
}
