using System;
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
            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddColumn<int>(
                name: "TotalSubscriptionMonth",
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
