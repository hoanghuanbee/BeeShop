using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Bee_Shop.Models;

public partial class BeeShopDbContext : DbContext
{
    public BeeShopDbContext()
    {
    }

    public BeeShopDbContext(DbContextOptions<BeeShopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Budget> Budgets { get; set; }

    public virtual DbSet<BudgetHistory> BudgetHistories { get; set; }

    public virtual DbSet<CartUser> CartUsers { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Medium> Media { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<StockImport> StockImports { get; set; }

    public virtual DbSet<StockImportsItem> StockImportsItems { get; set; }

    public virtual DbSet<Subcategory> Subcategories { get; set; }

    public virtual DbSet<Supply> Supplies { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=HOANGHUAN\\MSSQLSERVER01;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Initial Catalog=BeeShopDB;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Budget>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("budget");

            entity.Property(e => e.Budget1).HasColumnName("budget");
        });

        modelBuilder.Entity<BudgetHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__budget_h__3213E83F3E552B15");

            entity.ToTable("budget_history");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ImportId).HasColumnName("import_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Reason)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("reason");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<CartUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cart_use__3213E83F1BBCFF6F");

            entity.ToTable("cart_user");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__categori__3213E83FEEFDED84");

            entity.ToTable("categories");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("category_name");
            entity.Property(e => e.CategoryPosition).HasColumnName("category_position");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("slug");
            entity.Property(e => e.SupplyId).HasColumnName("supply_id");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__comments__3213E83FAFB1F1DE");

            entity.ToTable("comments");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Author)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("author");
            entity.Property(e => e.Content)
                .IsUnicode(false)
                .HasColumnName("content");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("createDate");
            entity.Property(e => e.EditTime)
                .HasColumnType("datetime")
                .HasColumnName("editTime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.LinkImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValue("author-comment.png")
                .HasColumnName("link_image");
            entity.Property(e => e.PageId)
                .HasDefaultValue(0)
                .HasColumnName("page_id");
            entity.Property(e => e.ParentCommentId).HasColumnName("parent_comment_id");
            entity.Property(e => e.PostId)
                .HasDefaultValue(0)
                .HasColumnName("post_id");
            entity.Property(e => e.ProductId)
                .HasDefaultValue(0)
                .HasColumnName("product_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__feedback__3213E83FB8C99EFF");

            entity.ToTable("feedbacks");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreateTime)
                .HasColumnType("datetime")
                .HasColumnName("createTime");
            entity.Property(e => e.EditTime)
                .HasColumnType("datetime")
                .HasColumnName("editTime");
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasDefaultValue("")
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Subject)
                .IsUnicode(false)
                .HasColumnName("subject");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Medium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__media__3213E83FF7908CB9");

            entity.ToTable("media");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("createDate");
            entity.Property(e => e.MediaName)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("media_name");
            entity.Property(e => e.Slug)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("slug");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__orders__3213E83FBA627C9A");

            entity.ToTable("orders");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.CartTotal).HasColumnName("cart_total");
            entity.Property(e => e.Createtime)
                .HasColumnType("datetime")
                .HasColumnName("createtime");
            entity.Property(e => e.Customer)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("customer");
            entity.Property(e => e.EditTime)
                .HasColumnType("datetime")
                .HasColumnName("editTime");
            entity.Property(e => e.Message)
                .IsUnicode(false)
                .HasColumnName("message");
            entity.Property(e => e.PaymentMethod).HasColumnName("payment_method");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Province)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("province");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__order_de__3213E83F0B905875");

            entity.ToTable("order_detail");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SupplyId).HasColumnName("supply_id");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__products__3213E83F4338A179");

            entity.ToTable("products");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("createBy");
            entity.Property(e => e.CreateDate).HasColumnName("createDate");
            entity.Property(e => e.EditBy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("editBy");
            entity.Property(e => e.EditDate)
                .HasColumnType("datetime")
                .HasColumnName("editDate");
            entity.Property(e => e.Img1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("img1");
            entity.Property(e => e.Img2)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("img2");
            entity.Property(e => e.Img3)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("img3");
            entity.Property(e => e.Img4)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("img4");
            entity.Property(e => e.Percentoff).HasColumnName("percentoff");
            entity.Property(e => e.ProductColor)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("product_color");
            entity.Property(e => e.ProductDescription)
                .IsUnicode(false)
                .HasColumnName("product_description");
            entity.Property(e => e.ProductDetail)
                .IsUnicode(false)
                .HasColumnName("product_detail");
            entity.Property(e => e.ProductMaterial)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("product_material");
            entity.Property(e => e.ProductName)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("product_name");
            entity.Property(e => e.ProductPrice).HasColumnName("product_price");
            entity.Property(e => e.ProductSize)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("product_size");
            entity.Property(e => e.ProductTypeid).HasColumnName("product_typeid");
            entity.Property(e => e.Saleoff)
                .HasDefaultValue((byte)0)
                .HasColumnName("saleoff");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("slug");
            entity.Property(e => e.SubCategoryId).HasColumnName("sub_category_id");
            entity.Property(e => e.SupplyId).HasColumnName("supply_id");
            entity.Property(e => e.TotalView)
                .HasDefaultValue(0)
                .HasColumnName("totalView");
        });

        modelBuilder.Entity<StockImport>(entity =>
        {
            entity.HasKey(e => e.ImportId).HasName("PK__stock_im__F3E6B05F0664C84E");

            entity.ToTable("stock_imports");

            entity.Property(e => e.ImportId)
                .ValueGeneratedNever()
                .HasColumnName("import_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ImportDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("import_date");
            entity.Property(e => e.InvoiceId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("invoice_id");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("supplier_name");
            entity.Property(e => e.TotalAmount).HasColumnName("total_amount");
        });

        modelBuilder.Entity<StockImportsItem>(entity =>
        {
            entity.HasKey(e => e.ImportItemId).HasName("PK__stock_im__68BACFC39494BD75");

            entity.ToTable("stock_imports_items");

            entity.Property(e => e.ImportItemId)
                .ValueGeneratedNever()
                .HasColumnName("import_item_id");
            entity.Property(e => e.ImportId).HasColumnName("import_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UnitCost).HasColumnName("unit_cost");
        });

        modelBuilder.Entity<Subcategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__subcateg__3213E83FA86EB819");

            entity.ToTable("subcategory");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Slug)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("slug");
            entity.Property(e => e.SubcategoryName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("subcategory_name");
            entity.Property(e => e.SupplyId)
                .HasDefaultValue(1)
                .HasColumnName("supply_id");
        });

        modelBuilder.Entity<Supply>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__supplies__3213E83FC3773C2B");

            entity.ToTable("supplies");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ImportId).HasColumnName("import_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UnitCost).HasColumnName("unit_cost");
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__types__3213E83FD7B3030C");

            entity.ToTable("types");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("slug");
            entity.Property(e => e.TypeDescription)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("type_description");
            entity.Property(e => e.TypeName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83F9CED86FB");

            entity.ToTable("users");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ConfirmationToken).HasMaxLength(255);
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("createDate");
            entity.Property(e => e.EditTime)
                .HasColumnType("datetime")
                .HasColumnName("editTime");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UserAddress)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("user_address");
            entity.Property(e => e.UserAvatar)
                .HasMaxLength(550)
                .IsUnicode(false)
                .HasDefaultValue("author-auto.png")
                .HasColumnName("user_avatar");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("user_email");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("user_name");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("user_password");
            entity.Property(e => e.UserPhone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("user_phone");
            entity.Property(e => e.UserUsername)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_username");
            entity.Property(e => e.VerificationCode)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("verificationCode");
            entity.Property(e => e.Verified).HasColumnName("verified");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
