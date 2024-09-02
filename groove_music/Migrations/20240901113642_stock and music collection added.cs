using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace groove_music.Migrations
{
    /// <inheritdoc />
    public partial class stockandmusiccollectionadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Albums",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Musics",
                columns: table => new
                {
                    MusicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MusicName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MusicCover = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MusicAlbumId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlbumsAlbumId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musics", x => x.MusicId);
                    table.ForeignKey(
                        name: "FK_Musics_Albums_AlbumsAlbumId",
                        column: x => x.AlbumsAlbumId,
                        principalTable: "Albums",
                        principalColumn: "AlbumId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Musics_AlbumsAlbumId",
                table: "Musics",
                column: "AlbumsAlbumId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Musics");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Albums");
        }
    }
}
