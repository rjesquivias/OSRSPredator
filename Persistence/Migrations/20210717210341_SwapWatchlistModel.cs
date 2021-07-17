using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class SwapWatchlistModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPriceSnapshots_WatchList_ItemAnalysisId",
                table: "ItemPriceSnapshots");

            migrationBuilder.DropIndex(
                name: "IX_ItemPriceSnapshots_ItemAnalysisId",
                table: "ItemPriceSnapshots");

            migrationBuilder.DropColumn(
                name: "ItemAnalysisId",
                table: "ItemPriceSnapshots");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ItemAnalysisId",
                table: "ItemPriceSnapshots",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemPriceSnapshots_ItemAnalysisId",
                table: "ItemPriceSnapshots",
                column: "ItemAnalysisId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPriceSnapshots_WatchList_ItemAnalysisId",
                table: "ItemPriceSnapshots",
                column: "ItemAnalysisId",
                principalTable: "WatchList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
