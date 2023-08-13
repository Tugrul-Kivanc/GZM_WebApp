using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseCreation.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Varsayılan Kategori"),
                    Price = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "EqualiventBrand",
                columns: table => new
                {
                    EqualiventBrandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Muadil Marka")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EqualiventBrand", x => x.EqualiventBrandId);
                });

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    NoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "?")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.NoteId);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Fee = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Payment = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValue: "Nakit"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "Perfume",
                columns: table => new
                {
                    PerfumeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValue: ""),
                    Brand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Bilinmeyen Marka"),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Bilinmeyen Tip"),
                    Smell = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "?"),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValue: "Unisex"),
                    Sillage = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: ""),
                    Weather = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, defaultValue: ""),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfume", x => x.PerfumeId);
                });

            migrationBuilder.CreateTable(
                name: "BaseNotes",
                columns: table => new
                {
                    PerfumeId = table.Column<int>(type: "int", nullable: false),
                    NoteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseNotes", x => new { x.PerfumeId, x.NoteId });
                    table.ForeignKey(
                        name: "FK_BaseNotes_Note_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Note",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseNotes_Perfume_PerfumeId",
                        column: x => x.PerfumeId,
                        principalTable: "Perfume",
                        principalColumn: "PerfumeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equalivent",
                columns: table => new
                {
                    EqualiventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "-"),
                    PerfumeId = table.Column<int>(type: "int", nullable: false),
                    EqualiventBrandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equalivent", x => x.EqualiventId);
                    table.ForeignKey(
                        name: "FK_Equalivent_EqualiventBrand_EqualiventBrandId",
                        column: x => x.EqualiventBrandId,
                        principalTable: "EqualiventBrand",
                        principalColumn: "EqualiventBrandId");
                    table.ForeignKey(
                        name: "FK_Equalivent_Perfume_PerfumeId",
                        column: x => x.PerfumeId,
                        principalTable: "Perfume",
                        principalColumn: "PerfumeId");
                });

            migrationBuilder.CreateTable(
                name: "MidNotes",
                columns: table => new
                {
                    PerfumeId = table.Column<int>(type: "int", nullable: false),
                    NoteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MidNotes", x => new { x.PerfumeId, x.NoteId });
                    table.ForeignKey(
                        name: "FK_MidNotes_Note_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Note",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MidNotes_Perfume_PerfumeId",
                        column: x => x.PerfumeId,
                        principalTable: "Perfume",
                        principalColumn: "PerfumeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Varsayılan Ürün"),
                    Stock = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TotalSales = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    PerfumeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId");
                    table.ForeignKey(
                        name: "FK_Product_Perfume_PerfumeId",
                        column: x => x.PerfumeId,
                        principalTable: "Perfume",
                        principalColumn: "PerfumeId");
                });

            migrationBuilder.CreateTable(
                name: "TopNotes",
                columns: table => new
                {
                    PerfumeId = table.Column<int>(type: "int", nullable: false),
                    NoteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopNotes", x => new { x.PerfumeId, x.NoteId });
                    table.ForeignKey(
                        name: "FK_TopNotes_Note_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Note",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TopNotes_Perfume_PerfumeId",
                        column: x => x.PerfumeId,
                        principalTable: "Perfume",
                        principalColumn: "PerfumeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrder",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrder", x => new { x.ProductId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_ProductOrder_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId");
                    table.ForeignKey(
                        name: "FK_ProductOrder_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseNotes_NoteId",
                table: "BaseNotes",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Equalivent_EqualiventBrandId",
                table: "Equalivent",
                column: "EqualiventBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Equalivent_PerfumeId",
                table: "Equalivent",
                column: "PerfumeId");

            migrationBuilder.CreateIndex(
                name: "IX_MidNotes_NoteId",
                table: "MidNotes",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_PerfumeId",
                table: "Product",
                column: "PerfumeId",
                unique: true,
                filter: "[PerfumeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrder_OrderId",
                table: "ProductOrder",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TopNotes_NoteId",
                table: "TopNotes",
                column: "NoteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseNotes");

            migrationBuilder.DropTable(
                name: "Equalivent");

            migrationBuilder.DropTable(
                name: "MidNotes");

            migrationBuilder.DropTable(
                name: "ProductOrder");

            migrationBuilder.DropTable(
                name: "TopNotes");

            migrationBuilder.DropTable(
                name: "EqualiventBrand");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Perfume");
        }
    }
}
