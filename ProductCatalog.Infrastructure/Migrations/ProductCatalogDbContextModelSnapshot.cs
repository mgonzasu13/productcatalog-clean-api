using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using ProductCatalog.Infrastructure.Data;

#nullable disable

namespace ProductCatalog.Infrastructure.Migrations;

[DbContext(typeof(ProductCatalogDbContext))]
partial class ProductCatalogDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.23")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

        modelBuilder.Entity("ProductCatalog.Domain.Entities.Product", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

            b.Property<DateTime>("CreatedAtUtc")
                .HasColumnType("datetime2");

            b.Property<string>("Description")
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnType("nvarchar(500)");

            b.Property<string>("Name")
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("nvarchar(150)");

            b.Property<decimal>("Price")
                .HasColumnType("decimal(18,2)");

            b.Property<int>("Stock")
                .HasColumnType("int");

            b.Property<DateTime?>("UpdatedAtUtc")
                .HasColumnType("datetime2");

            b.HasKey("Id");

            b.ToTable("Products");
        });
#pragma warning restore 612, 618
    }
}
