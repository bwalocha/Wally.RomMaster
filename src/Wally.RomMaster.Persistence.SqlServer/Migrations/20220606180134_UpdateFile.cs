using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wally.RomMaster.Persistence.SqlServer.Migrations
{
	public partial class UpdateFile : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "Crc",
				table: "[File]",
				type: "nvarchar(16)",
				maxLength: 16,
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(64)",
				oldMaxLength: 64,
				oldNullable: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "Crc",
				table: "[File]",
				type: "nvarchar(64)",
				maxLength: 64,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(16)",
				oldMaxLength: 16);
		}
	}
}
