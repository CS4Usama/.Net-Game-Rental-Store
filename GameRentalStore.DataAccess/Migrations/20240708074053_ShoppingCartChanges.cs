using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameRentalStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingCartChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_UserPackages_UserPackagesId",
                table: "ShoppingCarts");

            migrationBuilder.RenameColumn(
                name: "UserPackagesId",
                table: "ShoppingCarts",
                newName: "UserPackageId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCarts_UserPackagesId",
                table: "ShoppingCarts",
                newName: "IX_ShoppingCarts_UserPackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_UserPackages_UserPackageId",
                table: "ShoppingCarts",
                column: "UserPackageId",
                principalTable: "UserPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_UserPackages_UserPackageId",
                table: "ShoppingCarts");

            migrationBuilder.RenameColumn(
                name: "UserPackageId",
                table: "ShoppingCarts",
                newName: "UserPackagesId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCarts_UserPackageId",
                table: "ShoppingCarts",
                newName: "IX_ShoppingCarts_UserPackagesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_UserPackages_UserPackagesId",
                table: "ShoppingCarts",
                column: "UserPackagesId",
                principalTable: "UserPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
