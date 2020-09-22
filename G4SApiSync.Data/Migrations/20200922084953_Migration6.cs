using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EndPoint",
                schema: "g4s",
                table: "SyncResults",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AcademyCode",
                schema: "g4s",
                table: "SyncResults",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "StudentSessionSummaries",
                schema: "g4s",
                columns: table => new
                {
                    StudentId = table.Column<string>(maxLength: 100, nullable: false),
                    G4SStuId = table.Column<int>(nullable: false),
                    DataSet = table.Column<string>(maxLength: 4, nullable: true),
                    Academy = table.Column<string>(maxLength: 10, nullable: true),
                    PossibleSessions = table.Column<int>(nullable: false),
                    Present = table.Column<int>(nullable: false),
                    ApprovedEducationalActivity = table.Column<int>(nullable: false),
                    AuthorisedAbsence = table.Column<int>(nullable: false),
                    AttendanceNotRequired = table.Column<int>(nullable: false),
                    MissingMark = table.Column<int>(nullable: false),
                    Late = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSessionSummaries", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_StudentSessionSummaries_Students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "g4s",
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentSessionSummaries",
                schema: "g4s");

            migrationBuilder.AlterColumn<string>(
                name: "EndPoint",
                schema: "g4s",
                table: "SyncResults",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AcademyCode",
                schema: "g4s",
                table: "SyncResults",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);
        }
    }
}
