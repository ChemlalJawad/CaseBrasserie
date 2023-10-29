using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CaseBrasserie.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brasseries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brasseries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grossistes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grossistes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bieres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DegreAlcool = table.Column<float>(type: "real", nullable: false),
                    Prix = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BrasserieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bieres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bieres_Brasseries_BrasserieId",
                        column: x => x.BrasserieId,
                        principalTable: "Brasseries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GrossistesBieres",
                columns: table => new
                {
                    BiereId = table.Column<int>(type: "int", nullable: false),
                    GrossisteId = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrossistesBieres", x => new { x.BiereId, x.GrossisteId });
                    table.ForeignKey(
                        name: "FK_GrossistesBieres_Bieres_BiereId",
                        column: x => x.BiereId,
                        principalTable: "Bieres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrossistesBieres_Grossistes_GrossisteId",
                        column: x => x.GrossisteId,
                        principalTable: "Grossistes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brasseries",
                columns: new[] { "Id", "Nom" },
                values: new object[,]
                {
                    { 1, "Abbaye de Leffe" },
                    { 2, "Brasserie de Chimay" }
                });

            migrationBuilder.InsertData(
                table: "Grossistes",
                columns: new[] { "Id", "Nom" },
                values: new object[,]
                {
                    { 1, "GeneDrinks" },
                    { 2, "BiereImport" }
                });

            migrationBuilder.InsertData(
                table: "Bieres",
                columns: new[] { "Id", "BrasserieId", "DegreAlcool", "Nom", "Prix" },
                values: new object[,]
                {
                    { 1, 1, 6.6f, "Leffe Blonde", 2.20m },
                    { 2, 2, 9f, "Chimay Bleue", 3.50m },
                    { 3, 2, 7f, "Chimay Rouge", 3.00m }
                });

            migrationBuilder.InsertData(
                table: "GrossistesBieres",
                columns: new[] { "BiereId", "GrossisteId", "Stock" },
                values: new object[,]
                {
                    { 1, 1, 100 },
                    { 2, 1, 80 },
                    { 2, 2, 120 },
                    { 3, 2, 90 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bieres_BrasserieId",
                table: "Bieres",
                column: "BrasserieId");

            migrationBuilder.CreateIndex(
                name: "IX_GrossistesBieres_GrossisteId",
                table: "GrossistesBieres",
                column: "GrossisteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GrossistesBieres");

            migrationBuilder.DropTable(
                name: "Bieres");

            migrationBuilder.DropTable(
                name: "Grossistes");

            migrationBuilder.DropTable(
                name: "Brasseries");
        }
    }
}
