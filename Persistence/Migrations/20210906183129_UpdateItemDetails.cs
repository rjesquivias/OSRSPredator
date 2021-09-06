using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class UpdateItemDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchList");

            migrationBuilder.DropColumn(
                name: "WatchListItemDetailsId",
                table: "UserWatchList");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "WatchListItemDetailsId",
                table: "UserWatchList",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WatchList",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false),
                    examine = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    highalch = table.Column<long>(type: "INTEGER", nullable: false),
                    icon = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    limit = table.Column<long>(type: "INTEGER", nullable: false),
                    lowalch = table.Column<long>(type: "INTEGER", nullable: false),
                    members = table.Column<bool>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    prediction = table.Column<long>(type: "INTEGER", nullable: false),
                    value = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchList", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserWatchList_WatchListItemDetailsId",
                table: "UserWatchList",
                column: "WatchListItemDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWatchList_WatchList_WatchListItemDetailsId",
                table: "UserWatchList",
                column: "WatchListItemDetailsId",
                principalTable: "WatchList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
