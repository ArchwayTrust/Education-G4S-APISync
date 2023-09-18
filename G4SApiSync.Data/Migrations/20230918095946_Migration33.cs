using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace G4SApiSync.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migration33 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RemovedFromSource",
                schema: "g4s",
                table: "EducationDetails",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemovedFromSource",
                schema: "g4s",
                table: "EducationDetails");
        }
    }
}
