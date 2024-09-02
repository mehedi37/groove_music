using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace groove_music.Migrations
{
    /// <inheritdoc />
    public partial class musicModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musics_Albums_AlbumsAlbumId",
                table: "Musics");

            migrationBuilder.DropIndex(
                name: "IX_Musics_AlbumsAlbumId",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "AlbumsAlbumId",
                table: "Musics");

            migrationBuilder.AlterColumn<int>(
                name: "MusicAlbumId",
                table: "Musics",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Musics_MusicAlbumId",
                table: "Musics",
                column: "MusicAlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Musics_Albums_MusicAlbumId",
                table: "Musics",
                column: "MusicAlbumId",
                principalTable: "Albums",
                principalColumn: "AlbumId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musics_Albums_MusicAlbumId",
                table: "Musics");

            migrationBuilder.DropIndex(
                name: "IX_Musics_MusicAlbumId",
                table: "Musics");

            migrationBuilder.AlterColumn<string>(
                name: "MusicAlbumId",
                table: "Musics",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AlbumsAlbumId",
                table: "Musics",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Musics_AlbumsAlbumId",
                table: "Musics",
                column: "AlbumsAlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Musics_Albums_AlbumsAlbumId",
                table: "Musics",
                column: "AlbumsAlbumId",
                principalTable: "Albums",
                principalColumn: "AlbumId");
        }
    }
}
