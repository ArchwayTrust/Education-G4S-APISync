using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BehEventStudents",
                schema: "g4s",
                table: "BehEventStudents");

            migrationBuilder.DropColumn(
                name: "Academy",
                schema: "g4s",
                table: "BehEventStudents");

            migrationBuilder.DropColumn(
                name: "DataSet",
                schema: "g4s",
                table: "BehEventStudents");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                schema: "g4s",
                table: "BehEventStudents",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ModifiedByStaffId",
                schema: "g4s",
                table: "BehEvents",
                type: "int",
                maxLength: 100,
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_BehEventStudents",
                schema: "g4s",
                table: "BehEventStudents",
                columns: new[] { "BehEventId", "StudentId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BehEventStudents",
                schema: "g4s",
                table: "BehEventStudents");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                schema: "g4s",
                table: "BehEventStudents",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Academy",
                schema: "g4s",
                table: "BehEventStudents",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DataSet",
                schema: "g4s",
                table: "BehEventStudents",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedByStaffId",
                schema: "g4s",
                table: "BehEvents",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedByStaffId",
                schema: "g4s",
                table: "BehEvents",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BehEventStudents",
                schema: "g4s",
                table: "BehEventStudents",
                column: "BehEventId");
        }
    }
}
