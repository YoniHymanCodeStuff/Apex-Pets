using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Added_Orders_And_Adresses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Photos_ProfilePicId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProfilePicId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Admin_City",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Admin_Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfilePicId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "houseNumber",
                table: "Users",
                newName: "AdressId");

            migrationBuilder.RenameColumn(
                name: "Zip",
                table: "Users",
                newName: "CreditInfo");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Animal",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Required_Habitat",
                table: "Animal",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Required_License",
                table: "Animal",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeliveryAdress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    Street = table.Column<string>(type: "TEXT", nullable: true),
                    houseNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Zip = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryAdress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderTimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OrderStatus = table.Column<string>(type: "TEXT", nullable: true),
                    DeliveryTimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OrderedAnimalId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderedAnimalSpecies = table.Column<string>(type: "TEXT", nullable: true),
                    price = table.Column<decimal>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AdressId",
                table: "Users",
                column: "AdressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_DeliveryAdress_AdressId",
                table: "Users",
                column: "AdressId",
                principalTable: "DeliveryAdress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_DeliveryAdress_AdressId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "DeliveryAdress");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Users_AdressId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "Required_Habitat",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "Required_License",
                table: "Animal");

            migrationBuilder.RenameColumn(
                name: "CreditInfo",
                table: "Users",
                newName: "Zip");

            migrationBuilder.RenameColumn(
                name: "AdressId",
                table: "Users",
                newName: "houseNumber");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Admin_City",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Admin_Email",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfilePicId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfilePicId",
                table: "Users",
                column: "ProfilePicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Photos_ProfilePicId",
                table: "Users",
                column: "ProfilePicId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
