using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameRentalStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class StatusAddedInRatings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRatings_Games_GameId",
                table: "GameRatings");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "GameRatings");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "GameRatings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "GameRatings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_GameRatings_Games_GameId",
                table: "GameRatings",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameRatings_Games_GameId",
                table: "GameRatings");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "GameRatings");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "GameRatings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "GameRatings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_GameRatings_Games_GameId",
                table: "GameRatings",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");
        }
    }
}
