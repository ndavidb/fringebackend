using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fringe.Domain.Migrations
{
    /// <inheritdoc />
    public partial class VenueAndLocationTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoleName",
                table: "Roles",
                newName: "rolename");

            migrationBuilder.RenameColumn(
                name: "CanRead",
                table: "Roles",
                newName: "canread");

            migrationBuilder.RenameColumn(
                name: "CanEdit",
                table: "Roles",
                newName: "canedit");

            migrationBuilder.RenameColumn(
                name: "CanDelete",
                table: "Roles",
                newName: "candelete");

            migrationBuilder.RenameColumn(
                name: "CanCreate",
                table: "Roles",
                newName: "cancreate");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Roles",
                newName: "roleid");

            migrationBuilder.AlterColumn<string>(
                name: "rolename",
                table: "Roles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "canread",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "canedit",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "candelete",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "cancreate",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    locationid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    locationname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    suburb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    postalcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    latitude = table.Column<double>(type: "float", nullable: false),
                    longitude = table.Column<double>(type: "float", nullable: false),
                    parkingavailable = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    createdbyid = table.Column<int>(type: "int", nullable: false),
                    createdat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedid = table.Column<int>(type: "int", nullable: true),
                    updatedat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.locationid);
                });

            migrationBuilder.CreateTable(
                name: "VenueTypeLookup",
                columns: table => new
                {
                    typeid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    venuetype = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenueTypeLookup", x => x.typeid);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    venueid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    venuename = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    locationid = table.Column<int>(type: "int", nullable: false),
                    typeid = table.Column<int>(type: "int", nullable: false),
                    maxcapacity = table.Column<int>(type: "int", nullable: false),
                    seatingplanid = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contactemail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contactphone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isaccessible = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    venueurl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    createdbyid = table.Column<int>(type: "int", nullable: false),
                    createdat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedid = table.Column<int>(type: "int", nullable: true),
                    updatedat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.venueid);
                    table.ForeignKey(
                        name: "FK_Venues_Locations_locationid",
                        column: x => x.locationid,
                        principalTable: "Locations",
                        principalColumn: "locationid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Venues_VenueTypeLookup_typeid",
                        column: x => x.typeid,
                        principalTable: "VenueTypeLookup",
                        principalColumn: "typeid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_rolename",
                table: "Roles",
                column: "rolename",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Venues_locationid",
                table: "Venues",
                column: "locationid");

            migrationBuilder.CreateIndex(
                name: "IX_Venues_typeid",
                table: "Venues",
                column: "typeid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Venues");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "VenueTypeLookup");

            migrationBuilder.DropIndex(
                name: "IX_Roles_rolename",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "rolename",
                table: "Roles",
                newName: "RoleName");

            migrationBuilder.RenameColumn(
                name: "canread",
                table: "Roles",
                newName: "CanRead");

            migrationBuilder.RenameColumn(
                name: "canedit",
                table: "Roles",
                newName: "CanEdit");

            migrationBuilder.RenameColumn(
                name: "candelete",
                table: "Roles",
                newName: "CanDelete");

            migrationBuilder.RenameColumn(
                name: "cancreate",
                table: "Roles",
                newName: "CanCreate");

            migrationBuilder.RenameColumn(
                name: "roleid",
                table: "Roles",
                newName: "RoleId");

            migrationBuilder.AlterColumn<string>(
                name: "RoleName",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<bool>(
                name: "CanRead",
                table: "Roles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "CanEdit",
                table: "Roles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "CanDelete",
                table: "Roles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "CanCreate",
                table: "Roles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);
        }
    }
}
