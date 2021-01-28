using Microsoft.EntityFrameworkCore.Migrations;

namespace Wally.RomMaster.Database.Migrations
{
	public partial class RomCrcSizeIndex : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateIndex(name: "IX_Rom_Crc_Size", table: "Rom", columns: new[] { "Crc", "Size" });
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex(name: "IX_Rom_Crc_Size", table: "Rom");
		}
	}
}
