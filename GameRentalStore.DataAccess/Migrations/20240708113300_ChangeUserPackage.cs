using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameRentalStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserPackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPackages_SubscriptionPackages_PackageId",
                table: "UserPackages");

            migrationBuilder.AlterColumn<int>(
                name: "PackageId",
                table: "UserPackages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPackages_SubscriptionPackages_PackageId",
                table: "UserPackages",
                column: "PackageId",
                principalTable: "SubscriptionPackages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPackages_SubscriptionPackages_PackageId",
                table: "UserPackages");

            migrationBuilder.AlterColumn<int>(
                name: "PackageId",
                table: "UserPackages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPackages_SubscriptionPackages_PackageId",
                table: "UserPackages",
                column: "PackageId",
                principalTable: "SubscriptionPackages",
                principalColumn: "Id");
        }
    }
}
