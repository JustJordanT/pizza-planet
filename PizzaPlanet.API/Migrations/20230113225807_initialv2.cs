using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaPlanet.API.Migrations
{
    /// <inheritdoc />
    public partial class initialv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartEntity_CustomerEntity_CustomerId1",
                table: "CartEntity");

            migrationBuilder.DropIndex(
                name: "IX_CartEntity_CustomerId1",
                table: "CartEntity");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "CartEntity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerId1",
                table: "CartEntity",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CartEntity_CustomerId1",
                table: "CartEntity",
                column: "CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CartEntity_CustomerEntity_CustomerId1",
                table: "CartEntity",
                column: "CustomerId1",
                principalTable: "CustomerEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
