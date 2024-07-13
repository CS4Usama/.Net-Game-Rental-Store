using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameRentalStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class GameRatingIsApproved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "GameRating",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "GameRating");
        }
    }
}
