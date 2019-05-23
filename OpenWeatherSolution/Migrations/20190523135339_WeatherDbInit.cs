using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenWeatherSolution.Migrations
{
    public partial class WeatherDbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Weathers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Day = table.Column<DateTime>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    Wind = table.Column<double>(nullable: false),
                    Clouds = table.Column<double>(nullable: false),
                    Temperature = table.Column<double>(nullable: false),
                    TempMin = table.Column<double>(nullable: false),
                    TempMax = table.Column<double>(nullable: false),
                    Pressure = table.Column<double>(nullable: false),
                    Humidity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weathers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weathers");
        }
    }
}
