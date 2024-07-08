using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameRentalStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionPackageChagnes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "GamesPerMonth", "MaxReplacePerMonth", "PackageName", "RentNewReleasedGame" },
                values: new object[] { 3, 3, "Premium Max", 2 });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "GamesPerMonth", "MaxReplacePerMonth", "PackageName", "RentNewReleasedGame" },
                values: new object[] { 2, 2, "Premium", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "GamesPerMonth", "MaxReplacePerMonth", "PackageName", "RentNewReleasedGame" },
                values: new object[] { 2, 2, "Premium", 1 });

            migrationBuilder.UpdateData(
                table: "SubscriptionPackages",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "GamesPerMonth", "MaxReplacePerMonth", "PackageName", "RentNewReleasedGame" },
                values: new object[] { 3, 3, "Premium Max", 2 });
        }
    }
}
