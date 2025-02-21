using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MissionControlSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class PersonnelModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Telemetry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SpacecraftId = table.Column<int>(type: "INTEGER", nullable: false),
                    SatelliteId = table.Column<int>(type: "INTEGER", nullable: true),
                    MissionId = table.Column<int>(type: "INTEGER", nullable: false),
                    TelemetryDataType = table.Column<int>(type: "INTEGER", maxLength: 50, nullable: false),
                    Value = table.Column<decimal>(type: "TEXT", nullable: false),
                    Unit = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telemetry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Telemetry_Mission_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Mission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Telemetry_Satellite_SatelliteId",
                        column: x => x.SatelliteId,
                        principalTable: "Satellite",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Telemetry_Spacecraft_SpacecraftId",
                        column: x => x.SpacecraftId,
                        principalTable: "Spacecraft",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Telemetry_MissionId",
                table: "Telemetry",
                column: "MissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Telemetry_SatelliteId",
                table: "Telemetry",
                column: "SatelliteId");

            migrationBuilder.CreateIndex(
                name: "IX_Telemetry_SpacecraftId",
                table: "Telemetry",
                column: "SpacecraftId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Telemetry");
        }
    }
}
