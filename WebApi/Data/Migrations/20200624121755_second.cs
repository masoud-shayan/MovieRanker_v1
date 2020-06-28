using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieUserRanked_Movies_MovieId",
                table: "MovieUserRanked");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieUserRanked_Users_UserId",
                table: "MovieUserRanked");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieUserRanked",
                table: "MovieUserRanked");

            migrationBuilder.RenameTable(
                name: "MovieUserRanked",
                newName: "MovieUserRankeds");

            migrationBuilder.RenameIndex(
                name: "IX_MovieUserRanked_MovieId",
                table: "MovieUserRankeds",
                newName: "IX_MovieUserRankeds_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieUserRankeds",
                table: "MovieUserRankeds",
                columns: new[] { "UserId", "MovieId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MovieUserRankeds_Movies_MovieId",
                table: "MovieUserRankeds",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieUserRankeds_Users_UserId",
                table: "MovieUserRankeds",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieUserRankeds_Movies_MovieId",
                table: "MovieUserRankeds");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieUserRankeds_Users_UserId",
                table: "MovieUserRankeds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieUserRankeds",
                table: "MovieUserRankeds");

            migrationBuilder.RenameTable(
                name: "MovieUserRankeds",
                newName: "MovieUserRanked");

            migrationBuilder.RenameIndex(
                name: "IX_MovieUserRankeds_MovieId",
                table: "MovieUserRanked",
                newName: "IX_MovieUserRanked_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieUserRanked",
                table: "MovieUserRanked",
                columns: new[] { "UserId", "MovieId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MovieUserRanked_Movies_MovieId",
                table: "MovieUserRanked",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieUserRanked_Users_UserId",
                table: "MovieUserRanked",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
