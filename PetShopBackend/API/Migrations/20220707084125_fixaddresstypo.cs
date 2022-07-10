using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class fixaddresstypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Adresses_AdressId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "AdressId",
                table: "Users",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_AdressId",
                table: "Users",
                newName: "IX_Users_AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Adresses_AddressId",
                table: "Users",
                column: "AddressId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Adresses_AddressId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Users",
                newName: "AdressId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_AddressId",
                table: "Users",
                newName: "IX_Users_AdressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Adresses_AdressId",
                table: "Users",
                column: "AdressId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
