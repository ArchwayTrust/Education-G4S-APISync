using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarkslotMarks_Markslots_MockslotId",
                schema: "g4s",
                table: "MarkslotMarks");

            migrationBuilder.DropIndex(
                name: "IX_MarkslotMarks_MockslotId",
                schema: "g4s",
                table: "MarkslotMarks");

            migrationBuilder.DropColumn(
                name: "MockslotId",
                schema: "g4s",
                table: "MarkslotMarks");

            migrationBuilder.AddColumn<string>(
                name: "MarkslotId",
                schema: "g4s",
                table: "MarkslotMarks",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MarkslotMarks_MarkslotId",
                schema: "g4s",
                table: "MarkslotMarks",
                column: "MarkslotId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarkslotMarks_Markslots_MarkslotId",
                schema: "g4s",
                table: "MarkslotMarks",
                column: "MarkslotId",
                principalSchema: "g4s",
                principalTable: "Markslots",
                principalColumn: "MarkslotId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarkslotMarks_Markslots_MarkslotId",
                schema: "g4s",
                table: "MarkslotMarks");

            migrationBuilder.DropIndex(
                name: "IX_MarkslotMarks_MarkslotId",
                schema: "g4s",
                table: "MarkslotMarks");

            migrationBuilder.DropColumn(
                name: "MarkslotId",
                schema: "g4s",
                table: "MarkslotMarks");

            migrationBuilder.AddColumn<string>(
                name: "MockslotId",
                schema: "g4s",
                table: "MarkslotMarks",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MarkslotMarks_MockslotId",
                schema: "g4s",
                table: "MarkslotMarks",
                column: "MockslotId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarkslotMarks_Markslots_MockslotId",
                schema: "g4s",
                table: "MarkslotMarks",
                column: "MockslotId",
                principalSchema: "g4s",
                principalTable: "Markslots",
                principalColumn: "MarkslotId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
