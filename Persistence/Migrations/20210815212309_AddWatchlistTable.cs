using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddWatchlistTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WatchList",
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
                    name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    mostRecentSnapshotId = table.Column<string>(type: "TEXT", nullable: true),
                    prediction = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WatchList_ItemPriceSnapshots_mostRecentSnapshotId",
                        column: x => x.mostRecentSnapshotId,
                        principalTable: "ItemPriceSnapshots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WatchList_mostRecentSnapshotId",
                table: "WatchList",
                column: "mostRecentSnapshotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchList");
        }
    }
}
