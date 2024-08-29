using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLayer.Migrations
{
    /// <inheritdoc />
    public partial class updating_products : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Specifications",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Products",
                newName: "StockAmount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StockAmount",
                table: "Products",
                newName: "Gender");

            migrationBuilder.AddColumn<int[]>(
                name: "Size",
                table: "Products",
                type: "integer[]",
                nullable: true);

            migrationBuilder.AddColumn<int[]>(
                name: "Specifications",
                table: "Products",
                type: "integer[]",
                nullable: true);
        }
    }
}
