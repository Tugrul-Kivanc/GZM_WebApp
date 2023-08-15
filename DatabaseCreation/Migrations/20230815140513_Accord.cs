using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseCreation.Migrations
{
    /// <inheritdoc />
    public partial class Accord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccordId",
                table: "Note",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Accord",
                columns: table => new
                {
                    AccordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Varsayılan Akor"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accord", x => x.AccordId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Note_AccordId",
                table: "Note",
                column: "AccordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Accord_AccordId",
                table: "Note",
                column: "AccordId",
                principalTable: "Accord",
                principalColumn: "AccordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Note_Accord_AccordId",
                table: "Note");

            migrationBuilder.DropTable(
                name: "Accord");

            migrationBuilder.DropIndex(
                name: "IX_Note_AccordId",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "AccordId",
                table: "Note");
        }
    }
}
