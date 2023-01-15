using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaPlanet.API.Migrations
{
    /// <inheritdoc />
    public partial class initialv5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PizzaId",
                table: "CartEntity",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PizzasEntity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CrustType = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Toppings = table.Column<List<string>>(type: "text[]", nullable: false),
                    IsGlutenFree = table.Column<bool>(type: "boolean", nullable: false),
                    IsVegan = table.Column<bool>(type: "boolean", nullable: false),
                    IsVegetarian = table.Column<bool>(type: "boolean", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    CartId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzasEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PizzasEntity_CartEntity_CartId",
                        column: x => x.CartId,
                        principalTable: "CartEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PizzasEntity_CartId",
                table: "PizzasEntity",
                column: "CartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PizzasEntity");

            migrationBuilder.DropColumn(
                name: "PizzaId",
                table: "CartEntity");
        }
    }
}
