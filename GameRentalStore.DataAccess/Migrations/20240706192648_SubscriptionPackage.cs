using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameRentalStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionPackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubscriptionPackages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RentGame = table.Column<int>(type: "int", nullable: false),
                    RentNewReleasedGame = table.Column<int>(type: "int", nullable: false),
                    ReplaceGame = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPackages", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "SubscriptionPackages",
                columns: new[] { "Id", "PackageName", "RentGame", "RentNewReleasedGame", "ReplaceGame" },
                values: new object[,]
                {
                    { 1, "Basic", 2, 0, 0 },
                    { 2, "Premium", 2, 1, 2 },
                    { 3, "Premium Max", 3, 2, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionPackages");
        }
    }
}
