using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace G4SApiSync.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migration34 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxMarks",
                schema: "g4s",
                table: "Markslots",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxMarks",
                schema: "g4s",
                table: "Markslots");
        }
    }
}
