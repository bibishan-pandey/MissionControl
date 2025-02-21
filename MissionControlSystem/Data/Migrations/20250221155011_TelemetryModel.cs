using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MissionControlSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class TelemetryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AsteroidModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DiameterKm = table.Column<decimal>(type: "TEXT", nullable: false),
                    Composition = table.Column<string>(type: "TEXT", nullable: false),
                    DistanceFromEarthAu = table.Column<decimal>(type: "TEXT", nullable: false),
                    MissionId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsteroidModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AsteroidModel_Mission_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Mission",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AsteroidModel_MissionId",
                table: "AsteroidModel",
                column: "MissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AsteroidModel");
        }
    }
}
