﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttributes_EducationDetails_StudentId",
                schema: "g4s",
                table: "StudentAttributes");

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
                name: "FK_StudentAttributes_EducationDetails_EducationDetailStudentId",
                schema: "g4s",
                table: "StudentAttributes",
                column: "EducationDetailStudentId",
                principalSchema: "g4s",
                principalTable: "EducationDetails",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttributes_EducationDetails_EducationDetailStudentId",
                schema: "g4s",
                table: "StudentAttributes");

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
                name: "FK_StudentAttributes_EducationDetails_StudentId",
                schema: "g4s",
                table: "StudentAttributes",
                column: "StudentId",
                principalSchema: "g4s",
                principalTable: "EducationDetails",
                principalColumn: "StudentId");
        }
    }
}
