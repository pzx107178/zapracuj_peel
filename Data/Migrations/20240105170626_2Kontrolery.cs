using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mapkowanie.Data.Migrations
{
    public partial class _2Kontrolery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Konto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BranzaId = table.Column<int>(type: "int", nullable: true),
                    Zablokowane = table.Column<bool>(type: "bit", nullable: false),
                    RodzajKonta = table.Column<int>(type: "int", nullable: false),
                    WynagrodzenieMinimalne = table.Column<long>(type: "bigint", nullable: false),
                    GodzinaStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GodzinaStop = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nip = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Konto_Branza_BranzaId",
                        column: x => x.BranzaId,
                        principalTable: "Branza",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Oferta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KontoId = table.Column<int>(type: "int", nullable: true),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WynagrodzenieMin = table.Column<long>(type: "bigint", nullable: false),
                    WynagrodzenieMax = table.Column<long>(type: "bigint", nullable: false),
                    PracaStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PracaStop = table.Column<DateTime>(type: "datetime2", nullable: false),
                    widocznosc = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oferta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Oferta_Konto_KontoId",
                        column: x => x.KontoId,
                        principalTable: "Konto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Konto_BranzaId",
                table: "Konto",
                column: "BranzaId");

            migrationBuilder.CreateIndex(
                name: "IX_Oferta_KontoId",
                table: "Oferta",
                column: "KontoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Oferta");

            migrationBuilder.DropTable(
                name: "Konto");
        }
    }
}
