using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MissionControlSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AsteroidModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Satellite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    OrbitType = table.Column<int>(type: "INTEGER", maxLength: 20, nullable: false),
                    LaunchDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Operator = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "INTEGER", maxLength: 20, nullable: false),
                    SpacecraftId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Satellite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Satellite_Spacecraft_SpacecraftId",
                        column: x => x.SpacecraftId,
                        principalTable: "Spacecraft",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Satellite_SpacecraftId",
                table: "Satellite",
                column: "SpacecraftId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Satellite");
        }
    }
}
