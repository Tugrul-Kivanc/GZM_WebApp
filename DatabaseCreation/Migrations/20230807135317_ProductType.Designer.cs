﻿// <auto-generated />
using System;
using DatabaseCreation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DatabaseCreation.Migrations
{
    [DbContext(typeof(GZMWebAppDbContext))]
    [Migration("20230807135317_ProductType")]
    partial class ProductType
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DatabaseCreation.Accord", b =>
                {
                    b.Property<int>("AccordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccordId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("Akor Yok");

                    b.HasKey("AccordId");

                    b.ToTable("Accord");
                });

            modelBuilder.Entity("DatabaseCreation.Equalivent", b =>
                {
                    b.Property<int>("EqualiventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EqualiventId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("-");

                    b.Property<int>("EqualiventBrandId")
                        .HasColumnType("int");

                    b.Property<int>("PerfumeId")
                        .HasColumnType("int");

                    b.HasKey("EqualiventId");

                    b.HasIndex("EqualiventBrandId");

                    b.HasIndex("PerfumeId");

                    b.ToTable("Equalivent");
                });

            modelBuilder.Entity("DatabaseCreation.EqualiventBrand", b =>
                {
                    b.Property<int>("EqualiventBrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EqualiventBrandId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("Muadil Marka");

                    b.HasKey("EqualiventBrandId");

                    b.ToTable("EqualiventBrand");
                });

            modelBuilder.Entity("DatabaseCreation.Note", b =>
                {
                    b.Property<int>("NoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NoteId"));

                    b.Property<int>("AccordId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("?");

                    b.HasKey("NoteId");

                    b.HasIndex("AccordId");

                    b.ToTable("Note");
                });

            modelBuilder.Entity("DatabaseCreation.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<string>("Description")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<int>("Fee")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Payment")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasDefaultValue("Nakit");

                    b.Property<int>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("OrderId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("DatabaseCreation.Perfume", b =>
                {
                    b.Property<int>("PerfumeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PerfumeId"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("Bilinmeyen Marka");

                    b.Property<string>("Code")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasDefaultValue("");

                    b.Property<string>("Description")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasDefaultValue("Unisex");

                    b.Property<string>("Link")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<string>("Smell")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("?");

                    b.Property<string>("Type")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("Bilinmeyen Tip");

                    b.Property<string>("Weather")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasDefaultValue("");

                    b.HasKey("PerfumeId");

                    b.ToTable("Perfume");
                });

            modelBuilder.Entity("DatabaseCreation.PerfumeProduct", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("PerfumeId")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "PerfumeId");

                    b.HasIndex("PerfumeId")
                        .IsUnique();

                    b.ToTable("PerfumeProduct");
                });

            modelBuilder.Entity("DatabaseCreation.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValue("Varsayılan Ürün");

                    b.Property<int>("Price")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("Stock")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<long>("TotalSales")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(0L);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("NotePerfume", b =>
                {
                    b.Property<int>("NotesNoteId")
                        .HasColumnType("int");

                    b.Property<int>("PerfumesPerfumeId")
                        .HasColumnType("int");

                    b.HasKey("NotesNoteId", "PerfumesPerfumeId");

                    b.HasIndex("PerfumesPerfumeId");

                    b.ToTable("NotePerfume");
                });

            modelBuilder.Entity("OrderProduct", b =>
                {
                    b.Property<int>("OrdersOrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductsProductId")
                        .HasColumnType("int");

                    b.HasKey("OrdersOrderId", "ProductsProductId");

                    b.HasIndex("ProductsProductId");

                    b.ToTable("OrderProduct");
                });

            modelBuilder.Entity("DatabaseCreation.Equalivent", b =>
                {
                    b.HasOne("DatabaseCreation.EqualiventBrand", "EqualiventBrand")
                        .WithMany("Equalivents")
                        .HasForeignKey("EqualiventBrandId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DatabaseCreation.Perfume", "Perfume")
                        .WithMany("Equalivents")
                        .HasForeignKey("PerfumeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("EqualiventBrand");

                    b.Navigation("Perfume");
                });

            modelBuilder.Entity("DatabaseCreation.Note", b =>
                {
                    b.HasOne("DatabaseCreation.Accord", "Accord")
                        .WithMany("Notes")
                        .HasForeignKey("AccordId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Accord");
                });

            modelBuilder.Entity("DatabaseCreation.PerfumeProduct", b =>
                {
                    b.HasOne("DatabaseCreation.Perfume", "Perfume")
                        .WithOne("Product")
                        .HasForeignKey("DatabaseCreation.PerfumeProduct", "PerfumeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatabaseCreation.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Perfume");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("NotePerfume", b =>
                {
                    b.HasOne("DatabaseCreation.Note", null)
                        .WithMany()
                        .HasForeignKey("NotesNoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatabaseCreation.Perfume", null)
                        .WithMany()
                        .HasForeignKey("PerfumesPerfumeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OrderProduct", b =>
                {
                    b.HasOne("DatabaseCreation.Order", null)
                        .WithMany()
                        .HasForeignKey("OrdersOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatabaseCreation.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DatabaseCreation.Accord", b =>
                {
                    b.Navigation("Notes");
                });

            modelBuilder.Entity("DatabaseCreation.EqualiventBrand", b =>
                {
                    b.Navigation("Equalivents");
                });

            modelBuilder.Entity("DatabaseCreation.Perfume", b =>
                {
                    b.Navigation("Equalivents");

                    b.Navigation("Product")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
