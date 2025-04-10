using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fringe.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ShowsAgeRestrictionShowTypeStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgeRestrictionLookup",
                columns: table => new
                {
                    agerestrictionid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeRestrictionLookup", x => x.agerestrictionid);
                });

            migrationBuilder.CreateTable(
                name: "ShowTypeLookup",
                columns: table => new
                {
                    typeid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    showtype = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowTypeLookup", x => x.typeid);
                });

            migrationBuilder.CreateTable(
                name: "TicketTypes",
                columns: table => new
                {
                    tickettypeid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    typename = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTypes", x => x.tickettypeid);
                });

            migrationBuilder.CreateTable(
                name: "Shows",
                columns: table => new
                {
                    showid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    showname = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    venueid = table.Column<int>(type: "int", nullable: false),
                    showtypeid = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    agerestrictionid = table.Column<int>(type: "int", nullable: false),
                    warningdescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    startdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    enddate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tickettypeid = table.Column<int>(type: "int", nullable: true),
                    imagesurl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    videosurl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    createdbyid = table.Column<int>(type: "int", nullable: false),
                    createdat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedid = table.Column<int>(type: "int", nullable: true),
                    updatedat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => x.showid);
                    table.ForeignKey(
                        name: "FK_Shows_AgeRestrictionLookup_agerestrictionid",
                        column: x => x.agerestrictionid,
                        principalTable: "AgeRestrictionLookup",
                        principalColumn: "agerestrictionid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shows_ShowTypeLookup_showtypeid",
                        column: x => x.showtypeid,
                        principalTable: "ShowTypeLookup",
                        principalColumn: "typeid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shows_TicketTypes_tickettypeid",
                        column: x => x.tickettypeid,
                        principalTable: "TicketTypes",
                        principalColumn: "tickettypeid");
                    table.ForeignKey(
                        name: "FK_Shows_Venues_venueid",
                        column: x => x.venueid,
                        principalTable: "Venues",
                        principalColumn: "venueid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shows_agerestrictionid",
                table: "Shows",
                column: "agerestrictionid");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_showtypeid",
                table: "Shows",
                column: "showtypeid");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_tickettypeid",
                table: "Shows",
                column: "tickettypeid");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_venueid",
                table: "Shows",
                column: "venueid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shows");

            migrationBuilder.DropTable(
                name: "AgeRestrictionLookup");

            migrationBuilder.DropTable(
                name: "ShowTypeLookup");

            migrationBuilder.DropTable(
                name: "TicketTypes");
        }
    }
}
