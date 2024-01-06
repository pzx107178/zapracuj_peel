using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mapkowanie.Data.Migrations
{
    public partial class _375rodzajekont : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RodzajeId",
                table: "Konto",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Konto_RodzajeId",
                table: "Konto",
                column: "RodzajeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Konto_Rodzaje_RodzajeId",
                table: "Konto",
                column: "RodzajeId",
                principalTable: "Rodzaje",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Konto_Rodzaje_RodzajeId",
                table: "Konto");

            migrationBuilder.DropIndex(
                name: "IX_Konto_RodzajeId",
                table: "Konto");

            migrationBuilder.DropColumn(
                name: "RodzajeId",
                table: "Konto");
        }
    }
}
