using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bee_Shop.Migrations
{
    /// <inheritdoc />
    public partial class MergeCategoryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "category_children");

            migrationBuilder.DropTable(
                name: "category_parents");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SupplyId = table.Column<int>(type: "int", nullable: true),
                    CategoryPosition = table.Column<int>(type: "int", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Category_SupplyId",
                        column: x => x.SupplyId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_SupplyId",
                table: "Category",
                column: "SupplyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.CreateTable(
                name: "category_parents",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    category_position = table.Column<int>(type: "int", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category_parents", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "category_children",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    parent_id = table.Column<int>(type: "int", nullable: false),
                    category_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    category_position = table.Column<int>(type: "int", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category_children", x => x.id);
                    table.ForeignKey(
                        name: "FK_category_children_category_parents_parent_id",
                        column: x => x.parent_id,
                        principalTable: "category_parents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_category_children_parent_id",
                table: "category_children",
                column: "parent_id");
        }
    }
}
