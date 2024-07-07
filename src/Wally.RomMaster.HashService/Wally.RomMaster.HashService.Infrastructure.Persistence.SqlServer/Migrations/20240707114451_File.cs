using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wally.RomMaster.HashService.Infrastructure.Persistence.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class File : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "\"File\"",
                schema: "HashService",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    Length = table.Column<long>(type: "bigint", nullable: false),
                    Crc32 = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    Md5 = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Attributes = table.Column<int>(type: "int", nullable: false),
                    CreationTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastAccessTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastWriteTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_\"File\"", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_\"File\"_Crc32",
                schema: "HashService",
                table: "\"File\"",
                column: "Crc32");

            migrationBuilder.CreateIndex(
                name: "IX_\"File\"_Location",
                schema: "HashService",
                table: "\"File\"",
                column: "Location",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_\"File\"_Md5",
                schema: "HashService",
                table: "\"File\"",
                column: "Md5");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "\"File\"",
                schema: "HashService");
        }
    }
}
