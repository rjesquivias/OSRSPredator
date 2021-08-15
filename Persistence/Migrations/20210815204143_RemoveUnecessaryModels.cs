using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class RemoveUnecessaryModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchList");

            migrationBuilder.AddColumn<long>(
                name: "detailsId",
                table: "ItemDetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "mostRecentSnapshotId",
                table: "ItemDetails",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "prediction",
                table: "ItemDetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "snapshotId",
                table: "ItemDetails",
                type: "TEXT",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemDetails_ItemPriceSnapshots_mostRecentSnapshotId",
                table: "ItemDetails");

            migrationBuilder.DropIndex(
                name: "IX_ItemDetails_mostRecentSnapshotId",
                table: "ItemDetails");

            migrationBuilder.DropColumn(
                name: "detailsId",
                table: "ItemDetails");

            migrationBuilder.DropColumn(
                name: "mostRecentSnapshotId",
                table: "ItemDetails");

            migrationBuilder.DropColumn(
                name: "prediction",
                table: "ItemDetails");

            migrationBuilder.DropColumn(
                name: "snapshotId",
                table: "ItemDetails");

            migrationBuilder.CreateTable(
                name: "WatchList",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    delta = table.Column<long>(type: "INTEGER", nullable: false),
                    detailsId = table.Column<long>(type: "INTEGER", nullable: false),
                    prediction = table.Column<long>(type: "INTEGER", nullable: false),
                    snapshotId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WatchList_ItemDetails_detailsId",
                        column: x => x.detailsId,
                        principalTable: "ItemDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WatchList_ItemPriceSnapshots_snapshotId",
                        column: x => x.snapshotId,
                        principalTable: "ItemPriceSnapshots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WatchList_detailsId",
                table: "WatchList",
                column: "detailsId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchList_snapshotId",
                table: "WatchList",
                column: "snapshotId");
        }
    }
}
