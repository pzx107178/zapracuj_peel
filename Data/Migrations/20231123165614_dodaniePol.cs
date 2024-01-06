using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mapkowanie.Data.Migrations
{
    public partial class dodaniePol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Session",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Session_userId",
                table: "Session",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_AspNetUsers_userId",
                table: "Session",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_AspNetUsers_userId",
                table: "Session");

            migrationBuilder.DropIndex(
                name: "IX_Session_userId",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Session");
        }
    }
}
