using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class UpdateSimpleItemAnalysisModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchList_ItemDetails_itemDetailsId",
                table: "WatchList");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchList_ItemPriceSnapshots_mostRecentSnapshotId",
                table: "WatchList");

            migrationBuilder.DropIndex(
                name: "IX_WatchList_itemDetailsId",
                table: "WatchList");

            migrationBuilder.DropIndex(
                name: "IX_WatchList_mostRecentSnapshotId",
                table: "WatchList");

            migrationBuilder.DropColumn(
                name: "itemDetailsId",
                table: "WatchList");

            migrationBuilder.DropColumn(
                name: "mostRecentSnapshotId",
                table: "WatchList");

            migrationBuilder.AddColumn<long>(
                name: "detailsId",
                table: "WatchList",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "snapshotId",
                table: "WatchList",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WatchList_detailsId",
                table: "WatchList",
                column: "detailsId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchList_snapshotId",
                table: "WatchList",
                column: "snapshotId");

            migrationBuilder.AddForeignKey(
                name: "FK_WatchList_ItemDetails_detailsId",
                table: "WatchList",
                column: "detailsId",
                principalTable: "ItemDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchList_ItemPriceSnapshots_snapshotId",
                table: "WatchList",
                column: "snapshotId",
                principalTable: "ItemPriceSnapshots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchList_ItemDetails_detailsId",
                table: "WatchList");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchList_ItemPriceSnapshots_snapshotId",
                table: "WatchList");

            migrationBuilder.DropIndex(
                name: "IX_WatchList_detailsId",
                table: "WatchList");

            migrationBuilder.DropIndex(
                name: "IX_WatchList_snapshotId",
                table: "WatchList");

            migrationBuilder.DropColumn(
                name: "detailsId",
                table: "WatchList");

            migrationBuilder.DropColumn(
                name: "snapshotId",
                table: "WatchList");

            migrationBuilder.AddColumn<long>(
                name: "itemDetailsId",
                table: "WatchList",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "mostRecentSnapshotId",
                table: "WatchList",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WatchList_itemDetailsId",
                table: "WatchList",
                column: "itemDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchList_mostRecentSnapshotId",
                table: "WatchList",
                column: "mostRecentSnapshotId");

            migrationBuilder.AddForeignKey(
                name: "FK_WatchList_ItemDetails_itemDetailsId",
                table: "WatchList",
                column: "itemDetailsId",
                principalTable: "ItemDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchList_ItemPriceSnapshots_mostRecentSnapshotId",
                table: "WatchList",
                column: "mostRecentSnapshotId",
                principalTable: "ItemPriceSnapshots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
