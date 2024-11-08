using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MySite.Migrations
{
    /// <inheritdoc />
    public partial class WishList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_User_UserId1",
                table: "Carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");



            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Wishlist",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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
                name: "IX_Wishlist_UserId",
                table: "Wishlist",
                column: "UserId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "IX_Wishlist_UserId",
                table: "Wishlist");

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


            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Wishlist",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedUserName",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedEmail",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "User",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_User_UserId1",
                table: "Carts",
                column: "UserId1",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
