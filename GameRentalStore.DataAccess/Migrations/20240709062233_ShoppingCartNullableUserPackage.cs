using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameRentalStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingCartNullableUserPackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_UserPackages_UserPackageId",
                table: "ShoppingCarts");

            migrationBuilder.AlterColumn<int>(
                name: "UserPackageId",
                table: "ShoppingCarts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_UserPackages_UserPackageId",
                table: "ShoppingCarts",
                column: "UserPackageId",
                principalTable: "UserPackages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_UserPackages_UserPackageId",
                table: "ShoppingCarts");

            migrationBuilder.AlterColumn<int>(
                name: "UserPackageId",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_UserPackages_UserPackageId",
                table: "ShoppingCarts",
                column: "UserPackageId",
                principalTable: "UserPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
