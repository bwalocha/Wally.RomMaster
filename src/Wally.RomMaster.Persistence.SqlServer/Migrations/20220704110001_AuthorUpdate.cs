using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wally.RomMaster.Persistence.SqlServer.Migrations
{
	public partial class AuthorUpdate : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "Author",
				table: "DataFile",
				type: "nvarchar(1024)",
				maxLength: 1024,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(512)",
				oldMaxLength: 512,
				oldNullable: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "Author",
				table: "DataFile",
				type: "nvarchar(512)",
				maxLength: 512,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(1024)",
				oldMaxLength: 1024,
				oldNullable: true);
		}
	}
}
