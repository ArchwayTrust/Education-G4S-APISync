using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration31 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "sec",
                table: "AcademySecurity",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CurrentAcademicYear",
                schema: "sec",
                table: "AcademySecurity",
                maxLength: 4,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "APIKey",
                schema: "sec",
                table: "AcademySecurity",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                schema: "sec",
                table: "AcademySecurity",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HighestYear",
                schema: "sec",
                table: "AcademySecurity",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LowestYear",
                schema: "sec",
                table: "AcademySecurity",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                schema: "sec",
                table: "AcademySecurity");

            migrationBuilder.DropColumn(
                name: "HighestYear",
                schema: "sec",
                table: "AcademySecurity");

            migrationBuilder.DropColumn(
                name: "LowestYear",
                schema: "sec",
                table: "AcademySecurity");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "sec",
                table: "AcademySecurity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CurrentAcademicYear",
                schema: "sec",
                table: "AcademySecurity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "APIKey",
                schema: "sec",
                table: "AcademySecurity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
