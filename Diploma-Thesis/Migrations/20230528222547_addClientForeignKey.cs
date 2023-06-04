using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Diploma_Thesis.Migrations
{
    /// <inheritdoc />
    public partial class addClientForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Expertises_ClientId",
                table: "Expertises",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expertises_Clients_ClientId",
                table: "Expertises",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expertises_Clients_ClientId",
                table: "Expertises");

            migrationBuilder.DropIndex(
                name: "IX_Expertises_ClientId",
                table: "Expertises");
        }
    }
}
