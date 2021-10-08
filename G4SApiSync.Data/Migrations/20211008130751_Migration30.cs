using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BehEvents_BehEventTypes_BehEventTypeId",
                schema: "g4s",
                table: "BehEvents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_BehEvents_BehEventTypes_BehEventTypeId",
                schema: "g4s",
                table: "BehEvents",
                column: "BehEventTypeId",
                principalSchema: "g4s",
                principalTable: "BehEventTypes",
                principalColumn: "BehEventTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
