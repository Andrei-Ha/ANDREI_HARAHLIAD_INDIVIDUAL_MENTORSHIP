using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exadel.Forecast.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrentModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ForecastModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ForecastModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForecastModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForecastModels_CurrentModel_CurrentId",
                        column: x => x.CurrentId,
                        principalTable: "CurrentModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DayModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvgTemperature = table.Column<double>(type: "float", nullable: false),
                    MaxTemperature = table.Column<double>(type: "float", nullable: false),
                    MinTemperature = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ForecastModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayModel_ForecastModels_ForecastModelId",
                        column: x => x.ForecastModelId,
                        principalTable: "ForecastModels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrentModel_ForecastModelId",
                table: "CurrentModel",
                column: "ForecastModelId");

            migrationBuilder.CreateIndex(
                name: "IX_DayModel_ForecastModelId",
                table: "DayModel",
                column: "ForecastModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ForecastModels_CurrentId",
                table: "ForecastModels",
                column: "CurrentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentModel_ForecastModels_ForecastModelId",
                table: "CurrentModel",
                column: "ForecastModelId",
                principalTable: "ForecastModels",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentModel_ForecastModels_ForecastModelId",
                table: "CurrentModel");

            migrationBuilder.DropTable(
                name: "DayModel");

            migrationBuilder.DropTable(
                name: "ForecastModels");

            migrationBuilder.DropTable(
                name: "CurrentModel");
        }
    }
}
