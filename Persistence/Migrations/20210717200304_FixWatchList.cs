using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class FixWatchList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ItemAnalysisId",
                table: "ItemPriceSnapshots",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WatchList",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    delta = table.Column<long>(type: "INTEGER", nullable: false),
                    mostRecentSnapshotId = table.Column<Guid>(type: "TEXT", nullable: true),
                    itemDetailsId = table.Column<long>(type: "INTEGER", nullable: true),
                    prediction = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WatchList_ItemDetails_itemDetailsId",
                        column: x => x.itemDetailsId,
                        principalTable: "ItemDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WatchList_ItemPriceSnapshots_mostRecentSnapshotId",
                        column: x => x.mostRecentSnapshotId,
                        principalTable: "ItemPriceSnapshots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemPriceSnapshots_ItemAnalysisId",
                table: "ItemPriceSnapshots",
                column: "ItemAnalysisId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchList_itemDetailsId",
                table: "WatchList",
                column: "itemDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchList_mostRecentSnapshotId",
                table: "WatchList",
                column: "mostRecentSnapshotId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPriceSnapshots_WatchList_ItemAnalysisId",
                table: "ItemPriceSnapshots",
                column: "ItemAnalysisId",
                principalTable: "WatchList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPriceSnapshots_WatchList_ItemAnalysisId",
                table: "ItemPriceSnapshots");

            migrationBuilder.DropTable(
                name: "WatchList");

            migrationBuilder.DropIndex(
                name: "IX_ItemPriceSnapshots_ItemAnalysisId",
                table: "ItemPriceSnapshots");

            migrationBuilder.DropColumn(
                name: "ItemAnalysisId",
                table: "ItemPriceSnapshots");
        }
    }
}
