using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mapkowanie.Data.Migrations
{
    public partial class exercicesBothNowePola : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "ExcerciceType",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Excercice",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExcerciceType_userId",
                table: "ExcerciceType",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Excercice_userId",
                table: "Excercice",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Excercice_AspNetUsers_userId",
                table: "Excercice",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExcerciceType_AspNetUsers_userId",
                table: "ExcerciceType",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Excercice_AspNetUsers_userId",
                table: "Excercice");

            migrationBuilder.DropForeignKey(
                name: "FK_ExcerciceType_AspNetUsers_userId",
                table: "ExcerciceType");

            migrationBuilder.DropIndex(
                name: "IX_ExcerciceType_userId",
                table: "ExcerciceType");

            migrationBuilder.DropIndex(
                name: "IX_Excercice_userId",
                table: "Excercice");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "ExcerciceType");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Excercice");
        }
    }
}
