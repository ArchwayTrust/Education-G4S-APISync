using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeValues_AttributeTypes_AttributeTypeId",
                schema: "g4s",
                table: "AttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttributes_EducationDetails_StudentId",
                schema: "g4s",
                table: "StudentAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttributeValues_StudentAttributes_StudentAttributeId",
                schema: "g4s",
                table: "StudentAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Departments_DepartmentId",
                schema: "g4s",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentAttributes_StudentId",
                schema: "g4s",
                table: "StudentAttributes");

            migrationBuilder.AddColumn<string>(
                name: "EducationDetailStudentId",
                schema: "g4s",
                table: "StudentAttributes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttributes_EducationDetailStudentId",
                schema: "g4s",
                table: "StudentAttributes",
                column: "EducationDetailStudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeValues_AttributeTypes_AttributeTypeId",
                schema: "g4s",
                table: "AttributeValues",
                column: "AttributeTypeId",
                principalSchema: "g4s",
                principalTable: "AttributeTypes",
                principalColumn: "AttributeTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttributes_EducationDetails_EducationDetailStudentId",
                schema: "g4s",
                table: "StudentAttributes",
                column: "EducationDetailStudentId",
                principalSchema: "g4s",
                principalTable: "EducationDetails",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttributeValues_StudentAttributes_StudentAttributeId",
                schema: "g4s",
                table: "StudentAttributeValues",
                column: "StudentAttributeId",
                principalSchema: "g4s",
                principalTable: "StudentAttributes",
                principalColumn: "StudentAttributeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Departments_DepartmentId",
                schema: "g4s",
                table: "Subjects",
                column: "DepartmentId",
                principalSchema: "g4s",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeValues_AttributeTypes_AttributeTypeId",
                schema: "g4s",
                table: "AttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttributes_EducationDetails_EducationDetailStudentId",
                schema: "g4s",
                table: "StudentAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttributeValues_StudentAttributes_StudentAttributeId",
                schema: "g4s",
                table: "StudentAttributeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Departments_DepartmentId",
                schema: "g4s",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_StudentAttributes_EducationDetailStudentId",
                schema: "g4s",
                table: "StudentAttributes");

            migrationBuilder.DropColumn(
                name: "EducationDetailStudentId",
                schema: "g4s",
                table: "StudentAttributes");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttributes_StudentId",
                schema: "g4s",
                table: "StudentAttributes",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeValues_AttributeTypes_AttributeTypeId",
                schema: "g4s",
                table: "AttributeValues",
                column: "AttributeTypeId",
                principalSchema: "g4s",
                principalTable: "AttributeTypes",
                principalColumn: "AttributeTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttributes_EducationDetails_StudentId",
                schema: "g4s",
                table: "StudentAttributes",
                column: "StudentId",
                principalSchema: "g4s",
                principalTable: "EducationDetails",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttributeValues_StudentAttributes_StudentAttributeId",
                schema: "g4s",
                table: "StudentAttributeValues",
                column: "StudentAttributeId",
                principalSchema: "g4s",
                principalTable: "StudentAttributes",
                principalColumn: "StudentAttributeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Departments_DepartmentId",
                schema: "g4s",
                table: "Subjects",
                column: "DepartmentId",
                principalSchema: "g4s",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
