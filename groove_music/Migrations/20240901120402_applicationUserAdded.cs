using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace groove_music.Migrations
{
    /// <inheritdoc />
    public partial class applicationUserAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Albums",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_userId",
                table: "Albums",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_AspNetUsers_userId",
                table: "Albums",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_AspNetUsers_userId",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Albums_userId",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Albums");
        }
    }
}
