using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fringe.Domain.Migrations
{
    /// <inheritdoc />
    public partial class RefreshTokenExpiryDateNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpiresDate",
                table: "RefreshTokens",
                newName: "ExpiryDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpiryDate",
                table: "RefreshTokens",
                newName: "ExpiresDate");
        }
    }
}
