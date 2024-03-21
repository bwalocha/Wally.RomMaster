using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wally.RomMaster.FileService.Infrastructure.Persistence.SqlServer.Migrations
{
	/// <inheritdoc />
	public partial class Md5 : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "Md5",
				table: "\"File\"",
				type: "nvarchar(32)",
				maxLength: 32,
				nullable: true);

			migrationBuilder.CreateIndex(
				name: "IX_\"File\"_Md5",
				table: "\"File\"",
				column: "Md5");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex(
				name: "IX_\"File\"_Md5",
				table: "\"File\"");

			migrationBuilder.DropColumn(
				name: "Md5",
				table: "\"File\"");
		}
	}
}
