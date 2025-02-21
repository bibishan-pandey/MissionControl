using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MissionControlSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class FinalDraftModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personnel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PersonnelRole = table.Column<int>(type: "INTEGER", maxLength: 20, nullable: false),
                    MissionId = table.Column<int>(type: "INTEGER", nullable: true),
                    ControlSystemId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personnel_ControlSystem_ControlSystemId",
                        column: x => x.ControlSystemId,
                        principalTable: "ControlSystem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personnel_Mission_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Mission",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Personnel_ControlSystemId",
                table: "Personnel",
                column: "ControlSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnel_MissionId",
                table: "Personnel",
                column: "MissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Personnel");
        }
    }
}
