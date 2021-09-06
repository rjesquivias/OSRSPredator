using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class RemoveMostRecentSnapshot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemDetails_ItemPriceSnapshots_mostRecentSnapshotId",
                table: "ItemDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchList_ItemPriceSnapshots_mostRecentSnapshotId",
                table: "WatchList");

            migrationBuilder.DropIndex(
                name: "IX_WatchList_mostRecentSnapshotId",
                table: "WatchList");

            migrationBuilder.DropIndex(
                name: "IX_ItemDetails_mostRecentSnapshotId",
                table: "ItemDetails");

            migrationBuilder.DropColumn(
                name: "mostRecentSnapshotId",
                table: "WatchList");

            migrationBuilder.DropColumn(
                name: "mostRecentSnapshotId",
                table: "ItemDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "mostRecentSnapshotId",
                table: "WatchList",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "mostRecentSnapshotId",
                table: "ItemDetails",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WatchList_mostRecentSnapshotId",
                table: "WatchList",
                column: "mostRecentSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDetails_mostRecentSnapshotId",
                table: "ItemDetails",
                column: "mostRecentSnapshotId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemDetails_ItemPriceSnapshots_mostRecentSnapshotId",
                table: "ItemDetails",
                column: "mostRecentSnapshotId",
                principalTable: "ItemPriceSnapshots",
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
