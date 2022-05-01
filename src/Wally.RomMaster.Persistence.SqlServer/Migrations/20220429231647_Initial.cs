using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wally.RomMaster.Persistence.SqlServer.Migrations
{
	public partial class Initial : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "[File]",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Location = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
					Length = table.Column<long>(type: "bigint", nullable: false),
					Crc = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
					Sha1 = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
					Md5 = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
					Attributes = table.Column<int>(type: "int", nullable: false),
					CreationTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
					LastAccessTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
					LastWriteTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
					ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table => { table.PrimaryKey("PK_[File]", x => x.Id); });

			migrationBuilder.CreateTable(
				name: "User",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
				},
				constraints: table => { table.PrimaryKey("PK_User", x => x.Id); });

			migrationBuilder.CreateIndex(name: "IX_[File]_Location", table: "[File]", column: "Location", unique: true);

			migrationBuilder.CreateIndex(name: "IX_User_Name", table: "User", column: "Name", unique: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(name: "[File]");

			migrationBuilder.DropTable(name: "User");
		}
	}
}
