using Microsoft.EntityFrameworkCore;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Infrastructure.Data;

public sealed class ProductCatalogDbContext : DbContext
{
    public ProductCatalogDbContext(DbContextOptions<ProductCatalogDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");

            entity.HasKey(product => product.Id);

            entity.Property(product => product.Name)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(product => product.Description)
                .HasMaxLength(500)
                .IsRequired();

            entity.Property(product => product.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            entity.Property(product => product.Stock)
                .IsRequired();

            entity.Property(product => product.CreatedAtUtc)
                .IsRequired();

            entity.Property(product => product.UpdatedAtUtc);
        });
    }
}
