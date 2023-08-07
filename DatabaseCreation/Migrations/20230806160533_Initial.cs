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
                name: "Accord",
                columns: table => new
                {
                    AccordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Akor Yok")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accord", x => x.AccordId);
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
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Fee = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Payment = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValue: "Nakit"),
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
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValue: ""),
                    Brand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Bilinmeyen Marka"),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Bilinmeyen Tip"),
                    Smell = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "?"),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValue: "Unisex"),
                    Weather = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true, defaultValue: ""),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: ""),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfume", x => x.PerfumeId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Varsayılan Ürün"),
                    Stock = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Price = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TotalSales = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    NoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "?"),
                    AccordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_Note_Accord_AccordId",
                        column: x => x.AccordId,
                        principalTable: "Accord",
                        principalColumn: "AccordId");
                });

            migrationBuilder.CreateTable(
                name: "Equalivent",
                columns: table => new
                {
                    EqualiventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EqualiventBrandId = table.Column<int>(type: "int", nullable: false),
                    PerfumeId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "-")
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
                name: "OrderProduct",
                columns: table => new
                {
                    OrdersOrderId = table.Column<int>(type: "int", nullable: false),
                    ProductsProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => new { x.OrdersOrderId, x.ProductsProductId });
                    table.ForeignKey(
                        name: "FK_OrderProduct_Order_OrdersOrderId",
                        column: x => x.OrdersOrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Product_ProductsProductId",
                        column: x => x.ProductsProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerfumeProduct",
                columns: table => new
                {
                    PerfumeId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfumeProduct", x => new { x.ProductId, x.PerfumeId });
                    table.ForeignKey(
                        name: "FK_PerfumeProduct_Perfume_PerfumeId",
                        column: x => x.PerfumeId,
                        principalTable: "Perfume",
                        principalColumn: "PerfumeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerfumeProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotePerfume",
                columns: table => new
                {
                    NotesNoteId = table.Column<int>(type: "int", nullable: false),
                    PerfumesPerfumeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotePerfume", x => new { x.NotesNoteId, x.PerfumesPerfumeId });
                    table.ForeignKey(
                        name: "FK_NotePerfume_Note_NotesNoteId",
                        column: x => x.NotesNoteId,
                        principalTable: "Note",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotePerfume_Perfume_PerfumesPerfumeId",
                        column: x => x.PerfumesPerfumeId,
                        principalTable: "Perfume",
                        principalColumn: "PerfumeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equalivent_EqualiventBrandId",
                table: "Equalivent",
                column: "EqualiventBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Equalivent_PerfumeId",
                table: "Equalivent",
                column: "PerfumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_AccordId",
                table: "Note",
                column: "AccordId");

            migrationBuilder.CreateIndex(
                name: "IX_NotePerfume_PerfumesPerfumeId",
                table: "NotePerfume",
                column: "PerfumesPerfumeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductsProductId",
                table: "OrderProduct",
                column: "ProductsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfumeProduct_PerfumeId",
                table: "PerfumeProduct",
                column: "PerfumeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equalivent");

            migrationBuilder.DropTable(
                name: "NotePerfume");

            migrationBuilder.DropTable(
                name: "OrderProduct");

            migrationBuilder.DropTable(
                name: "PerfumeProduct");

            migrationBuilder.DropTable(
                name: "EqualiventBrand");

            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Perfume");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Accord");
        }
    }
}
