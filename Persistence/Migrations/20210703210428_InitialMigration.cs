using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false),
                    examine = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    members = table.Column<bool>(type: "INTEGER", nullable: false),
                    lowalch = table.Column<long>(type: "INTEGER", nullable: false),
                    limit = table.Column<long>(type: "INTEGER", nullable: false),
                    value = table.Column<long>(type: "INTEGER", nullable: false),
                    highalch = table.Column<long>(type: "INTEGER", nullable: false),
                    icon = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemHistoricals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemHistoricals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemPriceSnapshots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", maxLength: 70, nullable: false),
                    high = table.Column<long>(type: "INTEGER", nullable: false),
                    highTime = table.Column<long>(type: "INTEGER", nullable: false),
                    low = table.Column<long>(type: "INTEGER", nullable: false),
                    lowTime = table.Column<long>(type: "INTEGER", nullable: false),
                    ItemHistoricalId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPriceSnapshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemPriceSnapshots_ItemHistoricals_ItemHistoricalId",
                        column: x => x.ItemHistoricalId,
                        principalTable: "ItemHistoricals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemPriceSnapshots_ItemHistoricalId",
                table: "ItemPriceSnapshots",
                column: "ItemHistoricalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemDetails");

            migrationBuilder.DropTable(
                name: "ItemPriceSnapshots");

            migrationBuilder.DropTable(
                name: "ItemHistoricals");
        }
    }
}
