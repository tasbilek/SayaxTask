using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoltMeter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MonthlyRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    YekPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IndustrialEnergyTariff = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CommercialEnergyTariff = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IndustrialDistributionTariff = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CommercialDistributionTariff = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Municipals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalTax = table.Column<decimal>(type: "decimal(8,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PtfRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Period = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PtfRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    No = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Commission = table.Column<decimal>(type: "decimal(8,4)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(8,4)", nullable: true),
                    Vat = table.Column<decimal>(type: "decimal(8,4)", nullable: false),
                    Tariff = table.Column<int>(type: "int", nullable: false),
                    SalesMethod = table.Column<int>(type: "int", nullable: false),
                    MunicipalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meters_Municipals_MunicipalId",
                        column: x => x.MunicipalId,
                        principalTable: "Municipals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsumptionRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReadingDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReadingHour = table.Column<int>(type: "int", nullable: false),
                    ReadingValue = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    MeterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumptionRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsumptionRecords_Meters_MeterId",
                        column: x => x.MeterId,
                        principalTable: "Meters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptionRecords_MeterId",
                table: "ConsumptionRecords",
                column: "MeterId");

            migrationBuilder.CreateIndex(
                name: "IX_Meters_MunicipalId",
                table: "Meters",
                column: "MunicipalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumptionRecords");

            migrationBuilder.DropTable(
                name: "MonthlyRates");

            migrationBuilder.DropTable(
                name: "PtfRates");

            migrationBuilder.DropTable(
                name: "Meters");

            migrationBuilder.DropTable(
                name: "Municipals");
        }
    }
}
