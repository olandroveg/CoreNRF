using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreNRF.Migrations
{
    public partial class TblePortal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Portal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PortalName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LocationId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Portal_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PortalNF",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RelationName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PortalId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NFId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortalNF", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortalNF_NF_NFId",
                        column: x => x.NFId,
                        principalTable: "NF",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortalNF_Portal_PortalId",
                        column: x => x.PortalId,
                        principalTable: "Portal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Portal_LocationId",
                table: "Portal",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_PortalNF_NFId",
                table: "PortalNF",
                column: "NFId");

            migrationBuilder.CreateIndex(
                name: "IX_PortalNF_PortalId",
                table: "PortalNF",
                column: "PortalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortalNF");

            migrationBuilder.DropTable(
                name: "Portal");
        }
    }
}
