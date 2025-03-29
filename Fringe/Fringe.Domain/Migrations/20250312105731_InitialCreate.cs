using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fringe.Domain.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    roleid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rolename = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    cancreate = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    canread = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    canedit = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    candelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.roleid);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_rolename",
                table: "Roles",
                column: "rolename",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
