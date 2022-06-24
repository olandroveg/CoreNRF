using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreNRF.Migrations
{
    public partial class NFTbleField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SuscriptionAPI",
                table: "NF",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SuscriptionAPI",
                table: "NF");
        }
    }
}
