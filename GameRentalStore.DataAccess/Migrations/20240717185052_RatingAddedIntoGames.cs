using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameRentalStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RatingAddedIntoGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRating_AspNetUsers_ApplicationUserId",
                table: "GameRating");

            migrationBuilder.DropForeignKey(
                name: "FK_GameRating_ShoppingCarts_CartGameId",
                table: "GameRating");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameRating_GameRatingId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_GameRatingId",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameRating",
                table: "GameRating");

            migrationBuilder.DropColumn(
                name: "GameRatingId",
                table: "Games");

            migrationBuilder.RenameTable(
                name: "GameRating",
                newName: "GameRatings");

            migrationBuilder.RenameIndex(
                name: "IX_GameRating_CartGameId",
                table: "GameRatings",
                newName: "IX_GameRatings_CartGameId");

            migrationBuilder.RenameIndex(
                name: "IX_GameRating_ApplicationUserId",
                table: "GameRatings",
                newName: "IX_GameRatings_ApplicationUserId");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "GameRatings",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameRatings",
                table: "GameRatings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GameRatings_GameId",
                table: "GameRatings",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameRatings_AspNetUsers_ApplicationUserId",
                table: "GameRatings",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameRatings_Games_GameId",
                table: "GameRatings",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameRatings_ShoppingCarts_CartGameId",
                table: "GameRatings",
                column: "CartGameId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRatings_AspNetUsers_ApplicationUserId",
                table: "GameRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_GameRatings_Games_GameId",
                table: "GameRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_GameRatings_ShoppingCarts_CartGameId",
                table: "GameRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameRatings",
                table: "GameRatings");

            migrationBuilder.DropIndex(
                name: "IX_GameRatings_GameId",
                table: "GameRatings");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "GameRatings");

            migrationBuilder.RenameTable(
                name: "GameRatings",
                newName: "GameRating");

            migrationBuilder.RenameIndex(
                name: "IX_GameRatings_CartGameId",
                table: "GameRating",
                newName: "IX_GameRating_CartGameId");

            migrationBuilder.RenameIndex(
                name: "IX_GameRatings_ApplicationUserId",
                table: "GameRating",
                newName: "IX_GameRating_ApplicationUserId");

            migrationBuilder.AddColumn<int>(
                name: "GameRatingId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameRating",
                table: "GameRating",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "GameRatingId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                column: "GameRatingId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                column: "GameRatingId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4,
                column: "GameRatingId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5,
                column: "GameRatingId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Games_GameRatingId",
                table: "Games",
                column: "GameRatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameRating_AspNetUsers_ApplicationUserId",
                table: "GameRating",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameRating_ShoppingCarts_CartGameId",
                table: "GameRating",
                column: "CartGameId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameRating_GameRatingId",
                table: "Games",
                column: "GameRatingId",
                principalTable: "GameRating",
                principalColumn: "Id");
        }
    }
}
