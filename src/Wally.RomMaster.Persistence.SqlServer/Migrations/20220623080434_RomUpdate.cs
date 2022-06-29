using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wally.RomMaster.Persistence.SqlServer.Migrations
{
	public partial class RomUpdate : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "Crc",
				table: "Rom",
				type: "nvarchar(8)",
				maxLength: 8,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(8)",
				oldMaxLength: 8);

			migrationBuilder.CreateIndex(name: "IX_Rom_Crc", table: "Rom", column: "Crc");

			migrationBuilder.CreateIndex(name: "IX_[File]_Crc", table: "[File]", column: "Crc");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex(name: "IX_Rom_Crc", table: "Rom");

			migrationBuilder.DropIndex(name: "IX_[File]_Crc", table: "[File]");

			migrationBuilder.AlterColumn<string>(
				name: "Crc",
				table: "Rom",
				type: "nvarchar(8)",
				maxLength: 8,
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(8)",
				oldMaxLength: 8,
				oldNullable: true);
		}
	}
}
