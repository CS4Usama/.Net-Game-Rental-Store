using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameRentalStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddPackageIdIntoUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "TotalSubscriptionMonth",
                table: "ShoppingCarts",
                newName: "TotalSubscriptionMonths");

            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PackageId",
                table: "AspNetUsers",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_SubscriptionPackages_PackageId",
                table: "AspNetUsers",
                column: "PackageId",
                principalTable: "SubscriptionPackages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_SubscriptionPackages_PackageId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PackageId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "TotalSubscriptionMonths",
                table: "ShoppingCarts",
                newName: "TotalSubscriptionMonth");

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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
