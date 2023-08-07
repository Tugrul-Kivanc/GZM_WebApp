using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DatabaseModel.Models;

public partial class GZMWebAppDbContext : DbContext
{
    public GZMWebAppDbContext()
    {
    }

    public GZMWebAppDbContext(DbContextOptions<GZMWebAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Accord> Accords { get; set; }

    public virtual DbSet<Equalivent> Equalivents { get; set; }

    public virtual DbSet<EqualiventBrand> EqualiventBrands { get; set; }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Perfume> Perfumes { get; set; }

    public virtual DbSet<PerfumeProduct> PerfumeProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.;Database=GZMWebAppDb;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accord>(entity =>
        {
            entity.ToTable("Accord");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'Akor Yok')");
        });

        modelBuilder.Entity<Equalivent>(entity =>
        {
            entity.ToTable("Equalivent");

            entity.HasIndex(e => e.EqualiventBrandId, "IX_Equalivent_EqualiventBrandId");

            entity.HasIndex(e => e.PerfumeId, "IX_Equalivent_PerfumeId");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'-')");

            entity.HasOne(d => d.EqualiventBrand).WithMany(p => p.Equalivents)
                .HasForeignKey(d => d.EqualiventBrandId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Perfume).WithMany(p => p.Equalivents)
                .HasForeignKey(d => d.PerfumeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<EqualiventBrand>(entity =>
        {
            entity.ToTable("EqualiventBrand");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'Muadil Marka')");
        });

        modelBuilder.Entity<Note>(entity =>
        {
            entity.ToTable("Note");

            entity.HasIndex(e => e.AccordId, "IX_Note_AccordId");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'?')");

            entity.HasOne(d => d.Accord).WithMany(p => p.Notes)
                .HasForeignKey(d => d.AccordId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasMany(d => d.PerfumesPerfumes).WithMany(p => p.NotesNotes)
                .UsingEntity<Dictionary<string, object>>(
                    "NotePerfume",
                    r => r.HasOne<Perfume>().WithMany().HasForeignKey("PerfumesPerfumeId"),
                    l => l.HasOne<Note>().WithMany().HasForeignKey("NotesNoteId"),
                    j =>
                    {
                        j.HasKey("NotesNoteId", "PerfumesPerfumeId");
                        j.ToTable("NotePerfume");
                        j.HasIndex(new[] { "PerfumesPerfumeId" }, "IX_NotePerfume_PerfumesPerfumeId");
                    });
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.Description).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Payment)
                .HasMaxLength(10)
                .HasDefaultValueSql("(N'Nakit')");

            entity.HasMany(d => d.ProductsProducts).WithMany(p => p.OrdersOrders)
                .UsingEntity<Dictionary<string, object>>(
                    "OrderProduct",
                    r => r.HasOne<Product>().WithMany().HasForeignKey("ProductsProductId"),
                    l => l.HasOne<Order>().WithMany().HasForeignKey("OrdersOrderId"),
                    j =>
                    {
                        j.HasKey("OrdersOrderId", "ProductsProductId");
                        j.ToTable("OrderProduct");
                        j.HasIndex(new[] { "ProductsProductId" }, "IX_OrderProduct_ProductsProductId");
                    });
        });

        modelBuilder.Entity<Perfume>(entity =>
        {
            entity.ToTable("Perfume");

            entity.Property(e => e.Brand)
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'Bilinmeyen Marka')");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasDefaultValueSql("(N'')");
            entity.Property(e => e.Description).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasDefaultValueSql("(N'Unisex')");
            entity.Property(e => e.Link).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Smell)
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'?')");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'Bilinmeyen Tip')");
            entity.Property(e => e.Weather)
                .HasMaxLength(10)
                .HasDefaultValueSql("(N'')");
        });

        modelBuilder.Entity<PerfumeProduct>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.PerfumeId });

            entity.ToTable("PerfumeProduct");

            entity.HasIndex(e => e.PerfumeId, "IX_PerfumeProduct_PerfumeId").IsUnique();

            entity.HasOne(d => d.Perfume).WithOne(p => p.PerfumeProduct).HasForeignKey<PerfumeProduct>(d => d.PerfumeId);

            entity.HasOne(d => d.Product).WithMany(p => p.PerfumeProducts).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'Varsayılan Ürün')");
            entity.Property(e => e.TotalSales).HasDefaultValueSql("(CONVERT([bigint],(0)))");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'Varsayılan Ürün Tipi')");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
