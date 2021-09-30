using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BehEvents_BehEventTypes_BehEventTypeId",
                schema: "g4s",
                table: "BehEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_BehEventTypes_BehClassifications_BehClassificationId",
                schema: "g4s",
                table: "BehEventTypes");

            migrationBuilder.AddForeignKey(
                name: "FK_BehEvents_BehEventTypes_BehEventTypeId",
                schema: "g4s",
                table: "BehEvents",
                column: "BehEventTypeId",
                principalSchema: "g4s",
                principalTable: "BehEventTypes",
                principalColumn: "BehEventTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BehEventTypes_BehClassifications_BehClassificationId",
                schema: "g4s",
                table: "BehEventTypes",
                column: "BehClassificationId",
                principalSchema: "g4s",
                principalTable: "BehClassifications",
                principalColumn: "BehClassificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BehEvents_BehEventTypes_BehEventTypeId",
                schema: "g4s",
                table: "BehEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_BehEventTypes_BehClassifications_BehClassificationId",
                schema: "g4s",
                table: "BehEventTypes");

            migrationBuilder.AddForeignKey(
                name: "FK_BehEvents_BehEventTypes_BehEventTypeId",
                schema: "g4s",
                table: "BehEvents",
                column: "BehEventTypeId",
                principalSchema: "g4s",
                principalTable: "BehEventTypes",
                principalColumn: "BehEventTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BehEventTypes_BehClassifications_BehClassificationId",
                schema: "g4s",
                table: "BehEventTypes",
                column: "BehClassificationId",
                principalSchema: "g4s",
                principalTable: "BehClassifications",
                principalColumn: "BehClassificationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
