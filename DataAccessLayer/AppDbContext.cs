
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductModel> Products =>
            Set<ProductModel>();

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModel>(entity =>
            {
                entity.ToTable("products", "public");

                entity.HasKey(p => p.ProductId);

                entity.Property(p => p.ProductId)
                    .HasColumnName("product_id")
                    .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(p => p.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(p => p.Description)
                    .HasColumnName("description")
                    .HasMaxLength(500);

                entity.Property(p => p.Price)
                    .HasColumnName("price")
                    .HasPrecision(10, 2)
                    .IsRequired();

                entity.Property(p => p.StockQuantity)
                    .HasColumnName("stock_quantity")
                    .HasDefaultValue(0)
                    .IsRequired();

                entity.Property(p => p.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");
            });
        }
    }
}
