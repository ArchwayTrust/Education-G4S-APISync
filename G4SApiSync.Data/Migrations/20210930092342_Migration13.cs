using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teachers",
                schema: "g4s",
                columns: table => new
                {
                    TeacherId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    G4STeacherId = table.Column<int>(type: "int", nullable: false),
                    DataSet = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    Academy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PreferredFirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PreferredLastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Initials = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.TeacherId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teachers",
                schema: "g4s");
        }
    }
}
