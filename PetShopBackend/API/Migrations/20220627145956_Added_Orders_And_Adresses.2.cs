using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Added_Orders_And_Adresses2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_DeliveryAdress_AdressId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryAdress",
                table: "DeliveryAdress");

            migrationBuilder.RenameTable(
                name: "DeliveryAdress",
                newName: "Adresses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adresses",
                table: "Adresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Adresses_AdressId",
                table: "Users",
                column: "AdressId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Adresses_AdressId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adresses",
                table: "Adresses");

            migrationBuilder.RenameTable(
                name: "Adresses",
                newName: "DeliveryAdress");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryAdress",
                table: "DeliveryAdress",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_DeliveryAdress_AdressId",
                table: "Users",
                column: "AdressId",
                principalTable: "DeliveryAdress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
