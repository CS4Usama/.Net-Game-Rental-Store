using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameRentalStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UserPackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_SubscriptionPackages_PackageId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PackageId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "TotalSubscriptionMonths",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ReplaceGame",
                table: "SubscriptionPackages",
                newName: "MaxReplacePerMonth");

            migrationBuilder.RenameColumn(
                name: "RentGame",
                table: "SubscriptionPackages",
                newName: "GamesPerMonth");

            migrationBuilder.CreateTable(
                name: "UserPackages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PackageId = table.Column<int>(type: "int", nullable: true),
                    TotalSubscriptionMonths = table.Column<int>(type: "int", nullable: false),
                    SubscribedDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPackages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPackages_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPackages_SubscriptionPackages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "SubscriptionPackages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPackages_ApplicationUserId",
                table: "UserPackages",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPackages_PackageId",
                table: "UserPackages",
                column: "PackageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPackages");

            migrationBuilder.RenameColumn(
                name: "MaxReplacePerMonth",
                table: "SubscriptionPackages",
                newName: "ReplaceGame");

            migrationBuilder.RenameColumn(
                name: "GamesPerMonth",
                table: "SubscriptionPackages",
                newName: "RentGame");

            migrationBuilder.AddColumn<DateOnly>(
                name: "ReturnDate",
                table: "ShoppingCarts",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "TotalSubscriptionMonths",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
