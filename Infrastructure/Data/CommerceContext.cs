using System;
using System.Collections.Generic;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data;

public partial class CommerceContext : DbContext

{
    public CommerceContext()
    {
    }

    public CommerceContext(DbContextOptions<CommerceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BrandType> BrandTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }
    public virtual DbSet<BasketItem> BasketItems { get; set; }
    public virtual DbSet<customBasket> customBaskets { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BrandType>(entity =>
        {
            entity.ToTable("BrandType");

            entity.Property(e => e.BrandName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.BrandType).WithMany(p => p.Products)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_Product_BrandType");

            entity.HasOne(d => d.ProductType).WithMany(p => p.Products)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_Product_ProductType");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.ToTable("ProductType");

            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
