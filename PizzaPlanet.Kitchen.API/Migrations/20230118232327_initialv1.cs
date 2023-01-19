using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaPlanet.Kitchen.API.Migrations
{
    /// <inheritdoc />
    public partial class initialv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cook",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cook", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PizzasCompleted",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    PizzasIds = table.Column<string[]>(type: "text[]", nullable: false),
                    OrderId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzasCompleted", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrdersCompleted",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    OrderId = table.Column<string>(type: "text", nullable: false),
                    CookId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersCompleted", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdersCompleted_Cook_CookId",
                        column: x => x.CookId,
                        principalTable: "Cook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdersCompleted_CookId",
                table: "OrdersCompleted",
                column: "CookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdersCompleted");

            migrationBuilder.DropTable(
                name: "PizzasCompleted");

            migrationBuilder.DropTable(
                name: "Cook");
        }
    }
}
