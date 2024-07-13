using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameRentalStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class GameModelRatingNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameRating_GameRatingId",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "GameRatingId",
                table: "Games",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameRating_GameRatingId",
                table: "Games",
                column: "GameRatingId",
                principalTable: "GameRating",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameRating_GameRatingId",
                table: "Games");

            migrationBuilder.AlterColumn<int>(
                name: "GameRatingId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "GameRatingId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                column: "GameRatingId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                column: "GameRatingId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4,
                column: "GameRatingId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5,
                column: "GameRatingId",
                value: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameRating_GameRatingId",
                table: "Games",
                column: "GameRatingId",
                principalTable: "GameRating",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
