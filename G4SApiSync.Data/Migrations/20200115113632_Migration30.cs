using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupStudents_Groups_GroupId",
                schema: "g4s",
                table: "GroupStudents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                schema: "g4s",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "GroupsId",
                schema: "g4s",
                table: "Groups");

            migrationBuilder.AddColumn<string>(
                name: "GroupId",
                schema: "g4s",
                table: "Groups",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                schema: "g4s",
                table: "Groups",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupStudents_Groups_GroupId",
                schema: "g4s",
                table: "GroupStudents",
                column: "GroupId",
                principalSchema: "g4s",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupStudents_Groups_GroupId",
                schema: "g4s",
                table: "GroupStudents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                schema: "g4s",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "GroupId",
                schema: "g4s",
                table: "Groups");

            migrationBuilder.AddColumn<string>(
                name: "GroupsId",
                schema: "g4s",
                table: "Groups",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                schema: "g4s",
                table: "Groups",
                column: "GroupsId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupStudents_Groups_GroupId",
                schema: "g4s",
                table: "GroupStudents",
                column: "GroupId",
                principalSchema: "g4s",
                principalTable: "Groups",
                principalColumn: "GroupsId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
