using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    public partial class migrimi8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "FavoritePizzas");

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "Orders",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "Orders");

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "FavoritePizzas",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
