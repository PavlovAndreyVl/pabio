using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pabio.Migrations
{
    /// <inheritdoc />
    public partial class EventsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Caption",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Seq",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Caption",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Seq",
                table: "Events");
        }
    }
}
