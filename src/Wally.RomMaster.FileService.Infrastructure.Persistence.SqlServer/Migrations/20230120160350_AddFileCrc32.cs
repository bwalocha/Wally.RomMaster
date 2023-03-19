using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wally.RomMaster.FileService.Persistence.SqlServer.Migrations
{
	/// <inheritdoc />
	public partial class AddFileCrc32 : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "Crc32",
				table: "\"File\"",
				type: "nvarchar(16)",
				maxLength: 16,
				nullable: false,
				defaultValue: "");

			migrationBuilder.CreateIndex(name: "IX_\"File\"_Crc32", table: "\"File\"", column: "Crc32");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex(name: "IX_\"File\"_Crc32", table: "\"File\"");

			migrationBuilder.DropColumn(name: "Crc32", table: "\"File\"");
		}
	}
}
