using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class UserWatchList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserWatchList",
                columns: table => new
                {
                    AppUserId = table.Column<string>(type: "TEXT", nullable: false),
                    ItemDetailsId = table.Column<long>(type: "INTEGER", nullable: false),
                    MostRecentSnapshotId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWatchList", x => new { x.AppUserId, x.ItemDetailsId });
                    table.ForeignKey(
                        name: "FK_UserWatchList_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWatchList_ItemDetails_ItemDetailsId",
                        column: x => x.ItemDetailsId,
                        principalTable: "ItemDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWatchList_ItemPriceSnapshots_MostRecentSnapshotId",
                        column: x => x.MostRecentSnapshotId,
                        principalTable: "ItemPriceSnapshots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserWatchList_ItemDetailsId",
                table: "UserWatchList",
                column: "ItemDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWatchList_MostRecentSnapshotId",
                table: "UserWatchList",
                column: "MostRecentSnapshotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWatchList");
        }
    }
}
