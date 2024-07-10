using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameRentalStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingCartTableDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<DateOnly>(
                name: "RentedDate",
                table: "ShoppingCarts",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReturnDate",
                table: "ShoppingCarts",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_SubscriptionPackages_PackageId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_PackageId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "RentedDate",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "TotalSubscriptionMonth",
                table: "ShoppingCarts");
        }
    }
}
