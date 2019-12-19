using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SyncResults",
                schema: "g4s",
                columns: table => new
                {
                    SyncResultId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoggedAt = table.Column<DateTime>(nullable: false),
                    AcademyCode = table.Column<string>(maxLength: 500, nullable: true),
                    EndPoint = table.Column<string>(nullable: true),
                    Result = table.Column<bool>(nullable: false),
                    Exception = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyncResults", x => x.SyncResultId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SyncResults",
                schema: "g4s");
        }
    }
}
