using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wally.RomMaster.FileService.Persistence.SqlServer.Migrations
{
	/// <inheritdoc />
	public partial class UpdateFileCrc32 : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "Crc32",
				table: "\"File\"",
				type: "nvarchar(16)",
				maxLength: 16,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(16)",
				oldMaxLength: 16);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "Crc32",
				table: "\"File\"",
				type: "nvarchar(16)",
				maxLength: 16,
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(16)",
				oldMaxLength: 16,
				oldNullable: true);
		}
	}
}
