using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Diploma_Thesis.Migrations
{
    /// <inheritdoc />
    public partial class extenddiapasontable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diapasons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Element = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ElementRange = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ElementGrades = table.Column<byte>(type: "tinyint", nullable: false),
                    ProblemElement = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProblemElementRange = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProblemElementGrades = table.Column<byte>(type: "tinyint", nullable: false),
                    IsCrossing = table.Column<bool>(type: "bit", nullable: false),
                    Difference = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diapasons", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diapasons");
        }
    }
}
