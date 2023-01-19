using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaPlanet.Kitchen.API.Migrations
{
    /// <inheritdoc />
    public partial class initialv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "PizzasCompleted",
                newName: "CookId");

            migrationBuilder.AddColumn<string>(
                name: "OrderStatus",
                table: "OrdersCompleted",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PizzasCompleted_CookId",
                table: "PizzasCompleted",
                column: "CookId");

            migrationBuilder.AddForeignKey(
                name: "FK_PizzasCompleted_Cook_CookId",
                table: "PizzasCompleted",
                column: "CookId",
                principalTable: "Cook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PizzasCompleted_Cook_CookId",
                table: "PizzasCompleted");

            migrationBuilder.DropIndex(
                name: "IX_PizzasCompleted_CookId",
                table: "PizzasCompleted");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "OrdersCompleted");

            migrationBuilder.RenameColumn(
                name: "CookId",
                table: "PizzasCompleted",
                newName: "OrderId");
        }
    }
}
