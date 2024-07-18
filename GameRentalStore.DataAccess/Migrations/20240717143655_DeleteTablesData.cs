using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameRentalStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class DeleteTablesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 6);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Action" },
                    { 2, 2, "Adventure" },
                    { 3, 3, "Role Playing" },
                    { 4, 4, "Shooter" },
                    { 5, 5, "Simulator" },
                    { 6, 6, "Puzzle" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "GameRatingId", "GenreId", "Platform", "Quantity", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 1, "1975. Disaster strikes the Beira D oil rig off the coast of Scotland. Navigate the collapsing rig to save your crew from an otherworldly horror on the edge of all logic and reality.", null, 3, "PC", 7, new DateOnly(2024, 6, 18), "Still Wakes the Deep" },
                    { 2, "Burning with anger, The girl wages a one-woman war against the cult, taking them down cultist by cultist, bullet by bullet, until she reaches her true target: the leader.", null, 4, "Nintendo Switch", 10, new DateOnly(2024, 4, 9), "Children of the Sun" },
                    { 3, "Save the galaxy - one patient at a time! As the new director of Galacticare, you will build and manage a series of hospitals to keep your patients alive for as long as possible - for money!", null, 5, "PC", 8, new DateOnly(2024, 5, 23), "Galacticare" },
                    { 4, "Welcome to Botany Manor, a stately home in 19th century England. You play as its inhabitant Arabella Greene, a retired botanist.", null, 6, "Xbox", 6, new DateOnly(2024, 2, 9), "Botany Manor" },
                    { 5, "Stage dive into the heart of SoLA, the ultimate Californian music festival, built upon ancient grounds...", null, 3, "Playstation", 9, new DateOnly(2024, 3, 17), "Dead Island 2: SoLA" }
                });
        }
    }
}
