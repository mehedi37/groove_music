using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace groove_music.Migrations
{
    /// <inheritdoc />
    public partial class albumPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AlbumPrice",
                table: "Albums",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlbumPrice",
                table: "Albums");
        }
    }
}
