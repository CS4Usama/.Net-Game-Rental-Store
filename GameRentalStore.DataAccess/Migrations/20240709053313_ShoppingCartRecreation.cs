using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameRentalStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingCartRecreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_PackageId",
                table: "ShoppingCarts",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_SubscriptionPackages_PackageId",
                table: "ShoppingCarts",
                column: "PackageId",
                principalTable: "SubscriptionPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
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
        }
    }
}
