using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration29 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeValues",
                schema: "g4s",
                table: "AttributeValues");

            migrationBuilder.AlterColumn<string>(
                name: "AttributeTypeId",
                schema: "g4s",
                table: "AttributeValues",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                schema: "g4s",
                table: "AttributeValues",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "AttributeValueId",
                schema: "g4s",
                table: "AttributeValues",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeValues",
                schema: "g4s",
                table: "AttributeValues",
                column: "AttributeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeValues_StudentId",
                schema: "g4s",
                table: "AttributeValues",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeValues",
                schema: "g4s",
                table: "AttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_AttributeValues_StudentId",
                schema: "g4s",
                table: "AttributeValues");

            migrationBuilder.DropColumn(
                name: "AttributeValueId",
                schema: "g4s",
                table: "AttributeValues");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                schema: "g4s",
                table: "AttributeValues",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AttributeTypeId",
                schema: "g4s",
                table: "AttributeValues",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeValues",
                schema: "g4s",
                table: "AttributeValues",
                columns: new[] { "StudentId", "AttributeTypeId" });
        }
    }
}
