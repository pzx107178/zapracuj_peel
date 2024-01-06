using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mapkowanie.Data.Migrations
{
    public partial class _3User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Oferta",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Konto",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Oferta_userId",
                table: "Oferta",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Konto_userId",
                table: "Konto",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Konto_AspNetUsers_userId",
                table: "Konto",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Oferta_AspNetUsers_userId",
                table: "Oferta",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Konto_AspNetUsers_userId",
                table: "Konto");

            migrationBuilder.DropForeignKey(
                name: "FK_Oferta_AspNetUsers_userId",
                table: "Oferta");

            migrationBuilder.DropIndex(
                name: "IX_Oferta_userId",
                table: "Oferta");

            migrationBuilder.DropIndex(
                name: "IX_Konto_userId",
                table: "Konto");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Oferta");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Konto");
        }
    }
}
