using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace G4SApiSync.Data.Migrations
{
    public partial class Migration19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BehaviourFrom",
                schema: "sec",
                table: "AcademySecurity",
                type: "Date",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BehaviourTo",
                schema: "sec",
                table: "AcademySecurity",
                type: "Date",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GetBehaviour",
                schema: "sec",
                table: "AcademySecurity",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BehaviourFrom",
                schema: "sec",
                table: "AcademySecurity");

            migrationBuilder.DropColumn(
                name: "BehaviourTo",
                schema: "sec",
                table: "AcademySecurity");

            migrationBuilder.DropColumn(
                name: "GetBehaviour",
                schema: "sec",
                table: "AcademySecurity");
        }
    }
}
