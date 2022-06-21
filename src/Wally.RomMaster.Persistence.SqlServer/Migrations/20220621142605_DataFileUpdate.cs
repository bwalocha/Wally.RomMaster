using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wally.RomMaster.Persistence.SqlServer.Migrations
{
	public partial class DataFileUpdate : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "Category",
				table: "DataFile",
				type: "nvarchar(512)",
				maxLength: 512,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(512)",
				oldMaxLength: 512);

			migrationBuilder.AlterColumn<string>(
				name: "Author",
				table: "DataFile",
				type: "nvarchar(512)",
				maxLength: 512,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(512)",
				oldMaxLength: 512);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "Category",
				table: "DataFile",
				type: "nvarchar(512)",
				maxLength: 512,
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(512)",
				oldMaxLength: 512,
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "Author",
				table: "DataFile",
				type: "nvarchar(512)",
				maxLength: 512,
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(512)",
				oldMaxLength: 512,
				oldNullable: true);
		}
	}
}
