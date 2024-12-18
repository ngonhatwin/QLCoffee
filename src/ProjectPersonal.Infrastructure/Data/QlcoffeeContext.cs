using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProjectPersonal.Domain.Entities;

namespace ProjectPersonal;

public partial class QlcoffeeContext : DbContext
{
    public QlcoffeeContext()
    {
    }

    public QlcoffeeContext(DbContextOptions<QlcoffeeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderItem> OrderItems { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Cart> Carts { get; set; }
    public virtual DbSet<Reviews> Reviews { get; set; }
    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<User_Partition> Users_Partition { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MSI;Database=QLCoffee;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User_Partition>()
            .HasNoKey()
           .ToTable("user_partitioned", t => t.ExcludeFromMigrations());

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__orders__46596229562ED796");

            entity.ToTable("orders");

            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("order_id");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_amount");
            entity.Property(e => e.UserId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("user_id");
            entity.Property(e => e.CreatedDate)
                  .HasColumnType("datetime")
                  .HasColumnName("created_date");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.ModifiedDate)
                 .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("modified_by");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__orders__user_id__3E52440B");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__order_it__3764B6BC20574E91");

            entity.ToTable("order_items");

            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("order_item_id");
            entity.Property(e => e.OrderId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("order_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__order_ite__order__412EB0B6");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__order_ite__produ__4222D4EF");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__products__47027DF515DC49C9");

            entity.ToTable("products");

            entity.Property(e => e.Id)
                 .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("product_id");
            entity.Property(e => e.CategoryID)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("category_id");
            entity.Property(e => e.Description)
                .HasColumnType("nvarchar(max)")
                .HasColumnName("description")
                .IsUnicode(true);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(true)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Stock)
                .HasDefaultValue(0)
                .HasColumnName("stock");
            entity.Property(e => e.ImageURL)
                .HasColumnType("Text")
                .HasColumnName("image_url");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(true)
                .HasColumnName("created_by");
            entity.Property(e => e.ModifiedDate)
                 .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(true)
                .HasColumnName("modified_by");
            entity.HasOne(d => d.Categories)
               .WithMany(p => p.Products) // Một Category có nhiều Product
               .HasForeignKey(d => d.CategoryID) // Liên kết qua CategoryId
               .OnDelete(DeleteBehavior.Restrict) // Ngăn xóa Category nếu còn sản phẩm liên quan
               .HasConstraintName("FK_Product_Category");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__refresh___CB3C9E17F7FD50A1");

            entity.ToTable("refresh_tokens");

            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("token_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("datetime")
                .HasColumnName("expires_at");
            entity.Property(e => e.IsRevoked)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("is_revoked");
            entity.Property(e => e.RefreshTokenHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("refresh_token_hash");
            entity.Property(e => e.UserId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("user_id");
            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__refresh_t__user___44FF419A");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__B9BE370FB4ECFAE5");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E61646C862CAE").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__users__F3DBC572E85E12B4").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(true)
                .HasColumnName("full_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(255)
                .HasColumnName("phone");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(true)
                .HasColumnName("address");
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
            entity.Property(e => e.CreatedDate)
              .HasColumnType("datetime")
              .HasColumnName("created_date");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(true)
                .HasColumnName("created_by");
            entity.Property(e => e.ModifiedDate)
                 .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(true)
                .HasColumnName("modified_by");
        });
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.ProductId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("product_id");
            entity.Property(e => e.Quantity)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("quantity");
            entity.Property(e => e.CreatedDate)
             .HasColumnType("datetime")
             .HasColumnName("created_date");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(true)
                .HasColumnName("created_by");
            entity.Property(e => e.ModifiedDate)
                 .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(true)
                .HasColumnName("modified_by");
            // Thiết lập quan hệ với bảng Users
            entity.HasOne(d => d.User)
                .WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Cart_User");

            // Thiết lập quan hệ với bảng Products
            entity.HasOne(d => d.Product)
                .WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Cart_Product");
        });
        modelBuilder.Entity<Categories>(entity =>
        {
            entity.Property(e => e.Id)
                  .HasMaxLength(255)
                  .IsUnicode(false)
                  .HasColumnName("id");
            entity.Property(e => e.Name)
                  .HasMaxLength(255)
                  .IsUnicode(true)
                  .HasColumnName("name");
            entity.Property(e => e.Description)
                  .HasColumnName("description")
                  .IsUnicode(true);
        });
        modelBuilder.Entity<Reviews>(entity =>
        {
            entity.Property(e => e.Id)
            .HasMaxLength(255)
            .IsUnicode(false)
            .HasColumnName("id");
            entity.Property(e => e.ProductId)
            .HasMaxLength(255)
            .IsUnicode(false)
            .HasColumnName("product_id");
            entity.Property(e => e.UserId)
            .HasMaxLength(255)
            .IsUnicode(false)
            .HasColumnName ("user_id");
            entity.Property(e => e.Rating)
            .HasMaxLength(5)
            .IsUnicode(false)
            .HasColumnName("rating");
            entity.Property(e => e.Comment)
            .HasColumnType("text")
            .IsUnicode(true)
            .HasColumnName("comment");
            entity.Property(e => e.CreatedDate)
             .HasColumnType("datetime")
             .HasColumnName("created_date");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(true)
                .HasColumnName("created_by");
            entity.Property(e => e.ModifiedDate)
                 .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(true)
                .HasColumnName("modified_by");
            entity.HasOne(d => d.User)
                .WithMany(p => p.Reviews) // Một User có thể viết nhiều Reviews
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade) // Xóa User sẽ xóa Reviews
                .HasConstraintName("FK_Review_User");
            // Liên kết với Product
            entity.HasOne(d => d.Product)
                .WithMany(p => p.Reviews) // Một Product có thể có nhiều Reviews
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade) // Xóa Product sẽ xóa Reviews
                .HasConstraintName("FK_Review_Product");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
