using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BehEventStudents_BehEvents_EventBehEventId",
                schema: "g4s",
                table: "BehEventStudents");

            migrationBuilder.DropIndex(
                name: "IX_BehEventStudents_EventBehEventId",
                schema: "g4s",
                table: "BehEventStudents");

            migrationBuilder.DropColumn(
                name: "EventBehEventId",
                schema: "g4s",
                table: "BehEventStudents");

            migrationBuilder.RenameColumn(
                name: "EventTypeId",
                schema: "g4s",
                table: "BehEvents",
                newName: "BehEventTypeId");

            migrationBuilder.AlterColumn<string>(
                name: "YearGroup",
                schema: "g4s",
                table: "BehEvents",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedByStaffId",
                schema: "g4s",
                table: "BehEvents",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedByStaffId",
                schema: "g4s",
                table: "BehEvents",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_BehEventTypes_BehClassificationId",
                schema: "g4s",
                table: "BehEventTypes",
                column: "BehClassificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_BehEvents_BehEventTypes_BehEventId",
                schema: "g4s",
                table: "BehEvents",
                column: "BehEventId",
                principalSchema: "g4s",
                principalTable: "BehEventTypes",
                principalColumn: "BehEventTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BehEventStudents_BehEvents_BehEventId",
                schema: "g4s",
                table: "BehEventStudents",
                column: "BehEventId",
                principalSchema: "g4s",
                principalTable: "BehEvents",
                principalColumn: "BehEventId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BehEvents_BehEventTypes_BehEventId",
                schema: "g4s",
                table: "BehEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_BehEventStudents_BehEvents_BehEventId",
                schema: "g4s",
                table: "BehEventStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_BehEventTypes_BehClassifications_BehClassificationId",
                schema: "g4s",
                table: "BehEventTypes");

            migrationBuilder.DropIndex(
                name: "IX_BehEventTypes_BehClassificationId",
                schema: "g4s",
                table: "BehEventTypes");

            migrationBuilder.RenameColumn(
                name: "BehEventTypeId",
                schema: "g4s",
                table: "BehEvents",
                newName: "EventTypeId");

            migrationBuilder.AddColumn<int>(
                name: "EventBehEventId",
                schema: "g4s",
                table: "BehEventStudents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "YearGroup",
                schema: "g4s",
                table: "BehEvents",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ModifiedByStaffId",
                schema: "g4s",
                table: "BehEvents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByStaffId",
                schema: "g4s",
                table: "BehEvents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BehEventStudents_EventBehEventId",
                schema: "g4s",
                table: "BehEventStudents",
                column: "EventBehEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_BehEventStudents_BehEvents_EventBehEventId",
                schema: "g4s",
                table: "BehEventStudents",
                column: "EventBehEventId",
                principalSchema: "g4s",
                principalTable: "BehEvents",
                principalColumn: "BehEventId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
