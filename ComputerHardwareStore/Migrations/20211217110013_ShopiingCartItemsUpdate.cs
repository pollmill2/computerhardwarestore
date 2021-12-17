using Microsoft.EntityFrameworkCore.Migrations;

namespace ComputerHardwareStore.Migrations
{
    public partial class ShopiingCartItemsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShoppingCardItemId",
                table: "ShoppingCartItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShoppingCardItemId",
                table: "ShoppingCartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
