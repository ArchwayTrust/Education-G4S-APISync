using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BehEvents_BehEventTypes_BehEventId",
                schema: "g4s",
                table: "BehEvents");

            migrationBuilder.CreateIndex(
                name: "IX_BehEvents_BehEventTypeId",
                schema: "g4s",
                table: "BehEvents",
                column: "BehEventTypeId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BehEvents_BehEventTypes_BehEventTypeId",
                schema: "g4s",
                table: "BehEvents");

            migrationBuilder.DropIndex(
                name: "IX_BehEvents_BehEventTypeId",
                schema: "g4s",
                table: "BehEvents");

            migrationBuilder.AddForeignKey(
                name: "FK_BehEvents_BehEventTypes_BehEventId",
                schema: "g4s",
                table: "BehEvents",
                column: "BehEventId",
                principalSchema: "g4s",
                principalTable: "BehEventTypes",
                principalColumn: "BehEventTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
