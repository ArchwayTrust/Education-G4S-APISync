using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TTClasses",
                schema: "g4s",
                columns: table => new
                {
                    ClassId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataSet = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    Academy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    YearGroup = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SubjectCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GroupCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PeriodId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TTClasses", x => x.ClassId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TTClasses",
                schema: "g4s");
        }
    }
}
