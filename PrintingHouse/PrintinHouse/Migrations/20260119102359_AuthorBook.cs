using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintingHouse.Migrations
{
    /// <inheritdoc />
    public partial class AuthorBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorProduct_Author_AuthorsauthorId",
                table: "AuthorProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorProduct_Products_ProductsProductId",
                table: "AuthorProduct");

            migrationBuilder.RenameColumn(
                name: "ProductsProductId",
                table: "AuthorProduct",
                newName: "BookId");

            migrationBuilder.RenameColumn(
                name: "AuthorsauthorId",
                table: "AuthorProduct",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorProduct_ProductsProductId",
                table: "AuthorProduct",
                newName: "IX_AuthorProduct_BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorProduct_Author_AuthorId",
                table: "AuthorProduct",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "authorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorProduct_Products_BookId",
                table: "AuthorProduct",
                column: "BookId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorProduct_Author_AuthorId",
                table: "AuthorProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorProduct_Products_BookId",
                table: "AuthorProduct");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "AuthorProduct",
                newName: "ProductsProductId");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "AuthorProduct",
                newName: "AuthorsauthorId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorProduct_BookId",
                table: "AuthorProduct",
                newName: "IX_AuthorProduct_ProductsProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorProduct_Author_AuthorsauthorId",
                table: "AuthorProduct",
                column: "AuthorsauthorId",
                principalTable: "Author",
                principalColumn: "authorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorProduct_Products_ProductsProductId",
                table: "AuthorProduct",
                column: "ProductsProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
