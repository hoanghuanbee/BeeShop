using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bee_Shop.Migrations
{
    /// <inheritdoc />
    public partial class RebuildCategoryOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_SupplyId",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameColumn(
                name: "totalView",
                table: "products",
                newName: "TotalView");

            migrationBuilder.RenameColumn(
                name: "supply_id",
                table: "products",
                newName: "SupplyId");

            migrationBuilder.RenameColumn(
                name: "sub_category_id",
                table: "products",
                newName: "SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_SupplyId",
                table: "Categories",
                newName: "IX_Categories_SupplyId");

            migrationBuilder.AlterColumn<int>(
                name: "TotalView",
                table: "products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_SupplyId",
                table: "Categories",
                column: "SupplyId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_SupplyId",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "TotalView",
                table: "products",
                newName: "totalView");

            migrationBuilder.RenameColumn(
                name: "SupplyId",
                table: "products",
                newName: "supply_id");

            migrationBuilder.RenameColumn(
                name: "SubCategoryId",
                table: "products",
                newName: "sub_category_id");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_SupplyId",
                table: "Category",
                newName: "IX_Category_SupplyId");

            migrationBuilder.AlterColumn<int>(
                name: "totalView",
                table: "products",
                type: "int",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_SupplyId",
                table: "Category",
                column: "SupplyId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
