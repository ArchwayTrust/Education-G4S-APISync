using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PriorAttainment",
                schema: "g4s",
                table: "PriorAttainment");

            migrationBuilder.DropIndex(
                name: "IX_PriorAttainment_StudentId",
                schema: "g4s",
                table: "PriorAttainment");

            migrationBuilder.DropColumn(
                name: "PriorAttainmentId",
                schema: "g4s",
                table: "PriorAttainment");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                schema: "g4s",
                table: "PriorAttainment",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "g4s",
                table: "PriorAttainment",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriorAttainment",
                schema: "g4s",
                table: "PriorAttainment",
                columns: new[] { "StudentId", "Code" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PriorAttainment",
                schema: "g4s",
                table: "PriorAttainment");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "g4s",
                table: "PriorAttainment",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                schema: "g4s",
                table: "PriorAttainment",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "PriorAttainmentId",
                schema: "g4s",
                table: "PriorAttainment",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriorAttainment",
                schema: "g4s",
                table: "PriorAttainment",
                column: "PriorAttainmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PriorAttainment_StudentId",
                schema: "g4s",
                table: "PriorAttainment",
                column: "StudentId");
        }
    }
}
