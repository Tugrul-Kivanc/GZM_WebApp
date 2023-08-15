using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DatabaseModel.Models;

public partial class GzmdatabaseContext : DbContext
{
    public GzmdatabaseContext()
    {
    }

    public GzmdatabaseContext(DbContextOptions<GzmdatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Accord> Accords { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Equalivent> Equalivents { get; set; }

    public virtual DbSet<EqualiventBrand> EqualiventBrands { get; set; }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Perfume> Perfumes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=GZMDatabase;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accord>(entity =>
        {
            entity.ToTable("Accord");

            entity.Property(e => e.Description).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'Varsayılan Akor')");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'Varsayılan Kategori')");
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
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.Description).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Payment)
                .HasMaxLength(10)
                .HasDefaultValueSql("(N'Nakit')");
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
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasDefaultValueSql("(N'Unisex')");
            entity.Property(e => e.Info).HasDefaultValueSql("(N'')");
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

            entity.HasMany(d => d.Notes).WithMany(p => p.Perfumes)
                .UsingEntity<Dictionary<string, object>>(
                    "BaseNote",
                    r => r.HasOne<Note>().WithMany().HasForeignKey("NoteId"),
                    l => l.HasOne<Perfume>().WithMany().HasForeignKey("PerfumeId"),
                    j =>
                    {
                        j.HasKey("PerfumeId", "NoteId");
                        j.ToTable("BaseNotes");
                        j.HasIndex(new[] { "NoteId" }, "IX_BaseNotes_NoteId");
                    });

            entity.HasMany(d => d.Notes1).WithMany(p => p.Perfumes1)
                .UsingEntity<Dictionary<string, object>>(
                    "TopNote",
                    r => r.HasOne<Note>().WithMany().HasForeignKey("NoteId"),
                    l => l.HasOne<Perfume>().WithMany().HasForeignKey("PerfumeId"),
                    j =>
                    {
                        j.HasKey("PerfumeId", "NoteId");
                        j.ToTable("TopNotes");
                        j.HasIndex(new[] { "NoteId" }, "IX_TopNotes_NoteId");
                    });

            entity.HasMany(d => d.NotesNavigation).WithMany(p => p.PerfumesNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "MidNote",
                    r => r.HasOne<Note>().WithMany().HasForeignKey("NoteId"),
                    l => l.HasOne<Perfume>().WithMany().HasForeignKey("PerfumeId"),
                    j =>
                    {
                        j.HasKey("PerfumeId", "NoteId");
                        j.ToTable("MidNotes");
                        j.HasIndex(new[] { "NoteId" }, "IX_MidNotes_NoteId");
                    });
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.HasIndex(e => e.CategoryId, "IX_Product_CategoryId");

            entity.HasIndex(e => e.PerfumeId, "IX_Product_PerfumeId")
                .IsUnique()
                .HasFilter("([PerfumeId] IS NOT NULL)");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasDefaultValueSql("(N'Varsayılan Ürün')");
            entity.Property(e => e.TotalSales).HasDefaultValueSql("(CONVERT([bigint],(0)))");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Perfume).WithOne(p => p.Product).HasForeignKey<Product>(d => d.PerfumeId);

            entity.HasMany(d => d.Orders).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductOrder",
                    r => r.HasOne<Order>().WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("ProductId", "OrderId");
                        j.ToTable("ProductOrder");
                        j.HasIndex(new[] { "OrderId" }, "IX_ProductOrder_OrderId");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
