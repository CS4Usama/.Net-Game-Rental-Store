using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameRentalStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class GameRatingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "Platform", "GenreId", "GameRatingId", "ReleaseDate", "Title", "Quantity" },
                values: new object[,]
                {
                    { 1, "1975. Disaster strikes the Beira D oil rig off the coast of Scotland. Navigate the collapsing rig to save your crew from an otherworldly horror on the edge of all logic and reality.", "PC",2,  null,new DateOnly(2024, 6, 18), "Still Wakes the Deep",8 },
                    { 2, "Burning with anger, The girl wages a one-woman war against the cult, taking them down cultist by cultist, bullet by bullet, until she reaches her true target: the leader.", "Nintendo Switch",1, null,new DateOnly(2024, 4, 9), "Children of the Sun",8 },
                    { 3, "Save the galaxy - one patient at a time! As the new director of Galacticare, you will build and manage a series of hospitals to keep your patients alive for as long as possible - for money!", "PC",3, null, new DateOnly(2024, 5, 23), "Galacticare",8 },
                    { 4, "Welcome to Botany Manor, a stately home in 19th century England. You play as its inhabitant Arabella Greene, a retired botanist.", "Xbox",1, null, new DateOnly(2024, 4, 9), "Botany Manor",8 },
                    { 5, "Stage dive into the heart of SoLA, the ultimate Californian music festival, built upon ancient grounds...", "Playstation",2, null, new DateOnly(2024, 4, 17), "Dead Island 2: SoLA",8 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {


        }
    }
}
