using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration27 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentAttributeValues",
                schema: "g4s",
                table: "StudentAttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_StudentAttributeValues_StudentAttributeId",
                schema: "g4s",
                table: "StudentAttributeValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarkslotMarks",
                schema: "g4s",
                table: "MarkslotMarks");

            migrationBuilder.DropIndex(
                name: "IX_MarkslotMarks_StudentId",
                schema: "g4s",
                table: "MarkslotMarks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarksheetGrades",
                schema: "g4s",
                table: "MarksheetGrades");

            migrationBuilder.DropIndex(
                name: "IX_MarksheetGrades_StudentId",
                schema: "g4s",
                table: "MarksheetGrades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeValues",
                schema: "g4s",
                table: "AttributeValues");

            migrationBuilder.DropIndex(
                name: "IX_AttributeValues_StudentId",
                schema: "g4s",
                table: "AttributeValues");

            migrationBuilder.DropColumn(
                name: "StudentAttributeValueId",
                schema: "g4s",
                table: "StudentAttributeValues");

            migrationBuilder.DropColumn(
                name: "MarkslotMarkId",
                schema: "g4s",
                table: "MarkslotMarks");

            migrationBuilder.DropColumn(
                name: "MarksheetGradeId",
                schema: "g4s",
                table: "MarksheetGrades");

            migrationBuilder.DropColumn(
                name: "AttributeValueId",
                schema: "g4s",
                table: "AttributeValues");

            migrationBuilder.AlterColumn<string>(
                name: "StudentAttributeId",
                schema: "g4s",
                table: "StudentAttributeValues",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                schema: "g4s",
                table: "MarkslotMarks",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MarkslotId",
                schema: "g4s",
                table: "MarkslotMarks",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                schema: "g4s",
                table: "MarksheetGrades",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MarksheetId",
                schema: "g4s",
                table: "MarksheetGrades",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                schema: "g4s",
                table: "AttributeValues",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AttributeTypeId",
                schema: "g4s",
                table: "AttributeValues",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentAttributeValues",
                schema: "g4s",
                table: "StudentAttributeValues",
                column: "StudentAttributeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarkslotMarks",
                schema: "g4s",
                table: "MarkslotMarks",
                columns: new[] { "StudentId", "MarkslotId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarksheetGrades",
                schema: "g4s",
                table: "MarksheetGrades",
                columns: new[] { "StudentId", "MarksheetId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeValues",
                schema: "g4s",
                table: "AttributeValues",
                columns: new[] { "StudentId", "AttributeTypeId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentAttributeValues",
                schema: "g4s",
                table: "StudentAttributeValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarkslotMarks",
                schema: "g4s",
                table: "MarkslotMarks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarksheetGrades",
                schema: "g4s",
                table: "MarksheetGrades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeValues",
                schema: "g4s",
                table: "AttributeValues");

            migrationBuilder.AlterColumn<string>(
                name: "StudentAttributeId",
                schema: "g4s",
                table: "StudentAttributeValues",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "StudentAttributeValueId",
                schema: "g4s",
                table: "StudentAttributeValues",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "MarkslotId",
                schema: "g4s",
                table: "MarkslotMarks",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                schema: "g4s",
                table: "MarkslotMarks",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "MarkslotMarkId",
                schema: "g4s",
                table: "MarkslotMarks",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "MarksheetId",
                schema: "g4s",
                table: "MarksheetGrades",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                schema: "g4s",
                table: "MarksheetGrades",
                type: "nvarchar(100)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "MarksheetGradeId",
                schema: "g4s",
                table: "MarksheetGrades",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "AttributeTypeId",
                schema: "g4s",
                table: "AttributeValues",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                schema: "g4s",
                table: "AttributeValues",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "AttributeValueId",
                schema: "g4s",
                table: "AttributeValues",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentAttributeValues",
                schema: "g4s",
                table: "StudentAttributeValues",
                column: "StudentAttributeValueId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarkslotMarks",
                schema: "g4s",
                table: "MarkslotMarks",
                column: "MarkslotMarkId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarksheetGrades",
                schema: "g4s",
                table: "MarksheetGrades",
                column: "MarksheetGradeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeValues",
                schema: "g4s",
                table: "AttributeValues",
                column: "AttributeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttributeValues_StudentAttributeId",
                schema: "g4s",
                table: "StudentAttributeValues",
                column: "StudentAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_MarkslotMarks_StudentId",
                schema: "g4s",
                table: "MarkslotMarks",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_MarksheetGrades_StudentId",
                schema: "g4s",
                table: "MarksheetGrades",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeValues_StudentId",
                schema: "g4s",
                table: "AttributeValues",
                column: "StudentId");
        }
    }
}
