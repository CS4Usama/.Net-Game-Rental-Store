using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameRentalStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "GameMedias",
                columns: new[] { "Id", "GameId", "MediaType", "MediaUrl" },
                values: new object[,]
                {
                    { 1, 1, "image", "\\media\\games\\game-1\\4a77a065-a27f-4c79-9d45-90a0e6c7015b.webp" },
                    { 2, 1, "video", "https://www.youtube.com/embed/3svNksUg_BM" },
                    { 3, 1, "image", "\\media\\games\\game-1\\ea3d5b3c-c20a-4d4b-affc-0c4d0da61430.webp" },
                    { 4, 1, "image", "\\media\\games\\game-1\\0e07a077-5f67-4d7e-8c1e-863e5dcead70.webp" },
                    { 5, 1, "image", "\\media\\games\\game-1\\c6402a57-debe-48f6-8428-d60ad4997254.webp" },
                    { 6, 2, "image", "\\media\\games\\game-2\\e7378d53-6faa-47f8-8c67-b4ecfd9c41f9.webp" },
                    { 7, 2, "video", "https://www.youtube.com/embed/Jp4miWjVHtY" },
                    { 8, 2, "image", "\\media\\games\\game-2\\2ead7900-d473-47af-b367-93bb8a09c5f6.webp" },
                    { 9, 2, "image", "\\media\\games\\game-2\\94066954-7138-4a2e-924d-f149e300fb7a.webp" },
                    { 10, 2, "image", "\\media\\games\\game-2\\1ffb394b-aa03-44b4-a79a-bee9b771b05b.webp" },
                    { 11, 3, "image", "\\media\\games\\game-3\\8a2bb983-fda4-4015-9697-3442d9c989ee.webp" },
                    { 12, 3, "video", "https://www.youtube.com/embed/Qp0v7IVw4aA" },
                    { 13, 3, "image", "\\media\\games\\game-3\\77a06a13-f83a-40a0-af26-31ae3f8ef86e.webp" },
                    { 14, 3, "image", "\\media\\games\\game-3\\990afc77-95cd-41b5-9c28-4da1bf8a6688.webp" },
                    { 15, 3, "image", "\\media\\games\\game-3\\f5ad9b4e-08a0-453e-89eb-500aaf156685.webp" },
                    { 16, 4, "image", "\\media\\games\\game-4\\9972760f-bb59-4319-b145-357963e8f4a3.webp" },
                    { 17, 4, "video", "https://www.youtube.com/embed/vJcp6CMkUuA" },
                    { 18, 4, "image", "\\media\\games\\game-4\\9f30b7b1-aacb-42a9-a5f6-8473d7809567.webp" },
                    { 19, 4, "image", "\\media\\games\\game-4\\209c58e2-ff8d-4875-884e-f1e46e5e05af.webp" },
                    { 20, 4, "image", "\\media\\games\\game-4\\8d54d69b-2e58-4ea2-b71f-c6c9a10fc54b.webp" },
                    { 21, 5, "image", "\\media\\games\\game-5\\0cd268dc-36f5-488d-95e2-b2ce6c2791aa.webp" },
                    { 22, 5, "video", "https://www.youtube.com/embed/xC9FXqO5C2s" },
                    { 23, 5, "image", "\\media\\games\\game-5\\3a1fba0d-5b69-493f-8f3e-fc7572515f65.webp" },
                    { 24, 5, "image", "\\media\\games\\game-5\\ca3d1eb9-8750-495f-8337-ac4a92eda825.webp" },
                    { 25, 5, "image", "\\media\\games\\game-5\\c19de547-8615-4abd-a729-dca5d8fb601e.webp" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GameMedias",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GameMedias",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GameMedias",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GameMedias",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GameMedias",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "GameMedias",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "GameMedias",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "GameMedias",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "GameMedias",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "GameMedias",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "GameMedias",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "GameMedias",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "GameMedias",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "GameMedias",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "GameMedias",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "GameMedias",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "GameMedias",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "GameMedias",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "GameMedias",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "GameMedias",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "GameMedias",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "GameMedias",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "GameMedias",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "GameMedias",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "GameMedias",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

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
    }
}
