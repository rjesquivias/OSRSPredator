using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class RemovedFieldsFromItemDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "detailsId",
                table: "ItemDetails");

            migrationBuilder.DropColumn(
                name: "snapshotId",
                table: "ItemDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "detailsId",
                table: "ItemDetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "snapshotId",
                table: "ItemDetails",
                type: "TEXT",
                nullable: true);
        }
    }
}
