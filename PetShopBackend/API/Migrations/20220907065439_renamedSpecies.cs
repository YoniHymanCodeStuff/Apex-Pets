using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class renamedSpecies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderedAnimalSpecies",
                table: "Orders",
                newName: "OrderedAnimalName");

            migrationBuilder.RenameColumn(
                name: "Required_License",
                table: "Animal",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderedAnimalName",
                table: "Orders",
                newName: "OrderedAnimalSpecies");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Animal",
                newName: "Required_License");
        }
    }
}
