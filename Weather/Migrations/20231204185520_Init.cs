using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weather.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trashes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Time = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AirTemperature = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RelAirHumidity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DewPoint = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AtmPressure = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WindDirection = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WindSpeed = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CloudCover = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LowerCloudLimit = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HorizontalVisibility = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WeatherPhenomena = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FileFullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SheetName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RowNum = table.Column<int>(type: "int", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trashes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Weathers",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    AirTemperature = table.Column<double>(type: "float", nullable: false),
                    RelAirHumidity = table.Column<int>(type: "int", nullable: false),
                    DewPoint = table.Column<double>(type: "float", nullable: false),
                    AtmPressure = table.Column<int>(type: "int", nullable: false),
                    WindDirection = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    WindSpeed = table.Column<int>(type: "int", nullable: false),
                    CloudCover = table.Column<int>(type: "int", nullable: false),
                    LowerCloudLimit = table.Column<int>(type: "int", nullable: false),
                    HorizontalVisibility = table.Column<int>(type: "int", nullable: false),
                    WeatherPhenomena = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weathers", x => new { x.Date, x.Time });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trashes");

            migrationBuilder.DropTable(
                name: "Weathers");
        }
    }
}
