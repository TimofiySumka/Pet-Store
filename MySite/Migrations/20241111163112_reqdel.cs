using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MySite.Migrations
{
    /// <inheritdoc />
    public partial class reqdel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_AnimalType_AnimalTypeId1",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Brand_BrandId1",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryId1",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_AnimalTypeId1",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_BrandId1",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CategoryId1",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "AnimalTypeId1",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "BrandId1",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "Product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnimalTypeId1",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrandId1",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_AnimalTypeId1",
                table: "Product",
                column: "AnimalTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Product_BrandId1",
                table: "Product",
                column: "BrandId1");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId1",
                table: "Product",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_AnimalType_AnimalTypeId1",
                table: "Product",
                column: "AnimalTypeId1",
                principalTable: "AnimalType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Brand_BrandId1",
                table: "Product",
                column: "BrandId1",
                principalTable: "Brand",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId1",
                table: "Product",
                column: "CategoryId1",
                principalTable: "Category",
                principalColumn: "Id");
        }
    }
}
