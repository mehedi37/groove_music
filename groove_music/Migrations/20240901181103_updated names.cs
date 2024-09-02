using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace groove_music.Migrations
{
    /// <inheritdoc />
    public partial class updatednames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Artists_AlbumArtistId",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Musics_Albums_MusicAlbumId",
                table: "Musics");

            migrationBuilder.RenameColumn(
                name: "MusicAlbumId",
                table: "Musics",
                newName: "AlbumId");

            migrationBuilder.RenameIndex(
                name: "IX_Musics_MusicAlbumId",
                table: "Musics",
                newName: "IX_Musics_AlbumId");

            migrationBuilder.RenameColumn(
                name: "AlbumArtistId",
                table: "Albums",
                newName: "ArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_Albums_AlbumArtistId",
                table: "Albums",
                newName: "IX_Albums_ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Artists_ArtistId",
                table: "Albums",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "ArtistId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Musics_Albums_AlbumId",
                table: "Musics",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "AlbumId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Artists_ArtistId",
                table: "Albums");

            migrationBuilder.DropForeignKey(
                name: "FK_Musics_Albums_AlbumId",
                table: "Musics");

            migrationBuilder.RenameColumn(
                name: "AlbumId",
                table: "Musics",
                newName: "MusicAlbumId");

            migrationBuilder.RenameIndex(
                name: "IX_Musics_AlbumId",
                table: "Musics",
                newName: "IX_Musics_MusicAlbumId");

            migrationBuilder.RenameColumn(
                name: "ArtistId",
                table: "Albums",
                newName: "AlbumArtistId");

            migrationBuilder.RenameIndex(
                name: "IX_Albums_ArtistId",
                table: "Albums",
                newName: "IX_Albums_AlbumArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Artists_AlbumArtistId",
                table: "Albums",
                column: "AlbumArtistId",
                principalTable: "Artists",
                principalColumn: "ArtistId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Musics_Albums_MusicAlbumId",
                table: "Musics",
                column: "MusicAlbumId",
                principalTable: "Albums",
                principalColumn: "AlbumId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
