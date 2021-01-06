using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttendanceCodes",
                schema: "g4s",
                columns: table => new
                {
                    AttendanceCodeId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataSet = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    Academy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    AttendanceLabel = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    AttendanceOfficerOnly = table.Column<bool>(type: "bit", nullable: false),
                    ProtectAO = table.Column<bool>(type: "bit", nullable: false),
                    ProtectSM = table.Column<bool>(type: "bit", nullable: false),
                    ProtectBM = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceCodes", x => x.AttendanceCodeId);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceAliasCodes",
                schema: "g4s",
                columns: table => new
                {
                    AttendanceAliasCodeId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AttendanceCodeId = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    AliasCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Label = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceAliasCodes", x => x.AttendanceAliasCodeId);
                    table.ForeignKey(
                        name: "FK_AttendanceAliasCodes_AttendanceCodes_AttendanceCodeId",
                        column: x => x.AttendanceCodeId,
                        principalSchema: "g4s",
                        principalTable: "AttendanceCodes",
                        principalColumn: "AttendanceCodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceAliasCodes_AttendanceCodeId",
                schema: "g4s",
                table: "AttendanceAliasCodes",
                column: "AttendanceCodeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendanceAliasCodes",
                schema: "g4s");

            migrationBuilder.DropTable(
                name: "AttendanceCodes",
                schema: "g4s");
        }
    }
}
