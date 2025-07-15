using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bee_Shop.Migrations
{
    /// <inheritdoc />
    public partial class MyFirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "budget",
                columns: table => new
                {
                    budget = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "budget_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false),
                    reason = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    order_id = table.Column<int>(type: "int", nullable: true),
                    import_id = table.Column<int>(type: "int", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__budget_h__3213E83F3E552B15", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cart_user",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cart_use__3213E83F1BBCFF6F", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    category_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    supply_id = table.Column<int>(type: "int", nullable: true),
                    category_position = table.Column<int>(type: "int", nullable: true),
                    slug = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__categori__3213E83FEEFDED84", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    parent_comment_id = table.Column<int>(type: "int", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    createDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    author = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    link_image = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false, defaultValue: "author-comment.png"),
                    editTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    product_id = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    post_id = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    page_id = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__comments__3213E83FAFB1F1DE", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "feedbacks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    email = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false, defaultValue: ""),
                    phone = table.Column<long>(type: "bigint", nullable: true),
                    subject = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    createTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    order_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    editTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__feedback__3213E83FB8C99EFF", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "media",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    media_name = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    slug = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    createDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__media__3213E83FF7908CB9", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "order_detail",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    order_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    supply_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__order_de__3213E83F0B905875", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    customer = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    province = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    address = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    cart_total = table.Column<double>(type: "float", nullable: false),
                    createtime = table.Column<DateTime>(type: "datetime", nullable: false),
                    message = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    editTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    payment_method = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__orders__3213E83FBA627C9A", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    product_name = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    product_typeid = table.Column<int>(type: "int", nullable: false),
                    category_id = table.Column<int>(type: "int", nullable: true),
                    sub_category_id = table.Column<int>(type: "int", nullable: true),
                    supply_id = table.Column<int>(type: "int", nullable: true),
                    product_description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    product_price = table.Column<int>(type: "int", nullable: false),
                    product_color = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    product_material = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    product_size = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    product_detail = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    createBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    createDate = table.Column<DateOnly>(type: "date", nullable: true),
                    editBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    editDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    totalView = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    saleoff = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)0),
                    percentoff = table.Column<int>(type: "int", nullable: true),
                    img1 = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    img2 = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    img3 = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    img4 = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    slug = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__products__3213E83F4338A179", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "stock_imports",
                columns: table => new
                {
                    import_id = table.Column<int>(type: "int", nullable: false),
                    supplier_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    import_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    invoice_id = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    total_amount = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__stock_im__F3E6B05F0664C84E", x => x.import_id);
                });

            migrationBuilder.CreateTable(
                name: "stock_imports_items",
                columns: table => new
                {
                    import_item_id = table.Column<int>(type: "int", nullable: false),
                    import_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    unit_cost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__stock_im__68BACFC39494BD75", x => x.import_item_id);
                });

            migrationBuilder.CreateTable(
                name: "subcategory",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    subcategory_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    supply_id = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    category_id = table.Column<int>(type: "int", nullable: true),
                    slug = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__subcateg__3213E83FA86EB819", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "supplies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    import_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    unit_cost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__supplies__3213E83FC3773C2B", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    type_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    type_description = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    slug = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__types__3213E83FD7B3030C", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    user_username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    user_password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    user_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    user_avatar = table.Column<string>(type: "varchar(550)", unicode: false, maxLength: 550, nullable: true, defaultValue: "author-auto.png"),
                    user_email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    user_phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    user_address = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    createDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    verified = table.Column<int>(type: "int", nullable: false),
                    verificationCode = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    editTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    ConfirmationToken = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__users__3213E83F9CED86FB", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "budget");

            migrationBuilder.DropTable(
                name: "budget_history");

            migrationBuilder.DropTable(
                name: "cart_user");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "feedbacks");

            migrationBuilder.DropTable(
                name: "media");

            migrationBuilder.DropTable(
                name: "order_detail");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "stock_imports");

            migrationBuilder.DropTable(
                name: "stock_imports_items");

            migrationBuilder.DropTable(
                name: "subcategory");

            migrationBuilder.DropTable(
                name: "supplies");

            migrationBuilder.DropTable(
                name: "types");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
