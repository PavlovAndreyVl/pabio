using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pabio.Migrations
{
    /// <inheritdoc />
    public partial class DeletedEventsSeq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seq",
                table: "Events");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Seq",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
