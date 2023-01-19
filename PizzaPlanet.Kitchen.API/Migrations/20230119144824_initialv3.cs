using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaPlanet.Kitchen.API.Migrations
{
    /// <inheritdoc />
    public partial class initialv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PizzasIds",
                table: "PizzasCompleted");

            migrationBuilder.AddColumn<string>(
                name: "PizzaId",
                table: "PizzasCompleted",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PizzaId",
                table: "PizzasCompleted");

            migrationBuilder.AddColumn<string[]>(
                name: "PizzasIds",
                table: "PizzasCompleted",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);
        }
    }
}
