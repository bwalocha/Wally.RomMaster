using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wally.RomMaster.Persistence.SqlServer.Migrations
{
	public partial class FileSizeType : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<decimal>(
				name: "Size",
				table: "Rom",
				type: "decimal(20,0)",
				nullable: false,
				oldClrType: typeof(long),
				oldType: "bigint");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<long>(
				name: "Size",
				table: "Rom",
				type: "bigint",
				nullable: false,
				oldClrType: typeof(decimal),
				oldType: "decimal(20,0)");
		}
	}
}
