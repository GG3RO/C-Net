using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    public partial class migrimi2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoritePizza_Users_UserId",
                table: "FavoritePizza");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_pizzas_FavoritePizzaId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "pizzas");

            migrationBuilder.DropIndex(
                name: "IX_Orders_FavoritePizzaId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoritePizza",
                table: "FavoritePizza");

            migrationBuilder.DropIndex(
                name: "IX_FavoritePizza_UserId",
                table: "FavoritePizza");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "FavoritePizza",
                newName: "FavoritePizzas");

            migrationBuilder.RenameColumn(
                name: "FavoritePizzaId",
                table: "Orders",
                newName: "FavouritePizzaId");

            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "Orders",
                newName: "TOPPINGS");

            migrationBuilder.AddColumn<string>(
                name: "Crust",
                table: "Orders",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Method",
                table: "Orders",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "QTY",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Orders",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "TotalPrice",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoritePizzas",
                table: "FavoritePizzas",
                column: "FavoritePizzaId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FavouritePizzaId",
                table: "Orders",
                column: "FavouritePizzaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavoritePizzas_UserId",
                table: "FavoritePizzas",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritePizzas_Users_UserId",
                table: "FavoritePizzas",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_FavoritePizzas_FavouritePizzaId",
                table: "Orders",
                column: "FavouritePizzaId",
                principalTable: "FavoritePizzas",
                principalColumn: "FavoritePizzaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoritePizzas_Users_UserId",
                table: "FavoritePizzas");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_FavoritePizzas_FavouritePizzaId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_FavouritePizzaId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoritePizzas",
                table: "FavoritePizzas");

            migrationBuilder.DropIndex(
                name: "IX_FavoritePizzas_UserId",
                table: "FavoritePizzas");

            migrationBuilder.DropColumn(
                name: "Crust",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Method",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "QTY",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "FavoritePizzas",
                newName: "FavoritePizza");

            migrationBuilder.RenameColumn(
                name: "TOPPINGS",
                table: "Orders",
                newName: "CustomerName");

            migrationBuilder.RenameColumn(
                name: "FavouritePizzaId",
                table: "Orders",
                newName: "FavoritePizzaId");

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoritePizza",
                table: "FavoritePizza",
                column: "FavoritePizzaId");

            migrationBuilder.CreateTable(
                name: "pizzas",
                columns: table => new
                {
                    PizzaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    Crust = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Method = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QTY = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TOPPINGS = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pizzas", x => x.PizzaId);
                    table.ForeignKey(
                        name: "FK_pizzas_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FavoritePizzaId",
                table: "Orders",
                column: "FavoritePizzaId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoritePizza_UserId",
                table: "FavoritePizza",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_pizzas_OrderId",
                table: "pizzas",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritePizza_Users_UserId",
                table: "FavoritePizza",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_pizzas_FavoritePizzaId",
                table: "Orders",
                column: "FavoritePizzaId",
                principalTable: "pizzas",
                principalColumn: "PizzaId");
        }
    }
}
