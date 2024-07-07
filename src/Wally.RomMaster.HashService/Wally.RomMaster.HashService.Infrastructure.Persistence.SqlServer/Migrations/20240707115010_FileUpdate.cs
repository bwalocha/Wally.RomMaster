using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wally.RomMaster.HashService.Infrastructure.Persistence.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class FileUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_\"File\"_Location",
                schema: "HashService",
                table: "\"File\"");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_\"File\"_Location",
                schema: "HashService",
                table: "\"File\"",
                column: "Location",
                unique: true);
        }
    }
}
