using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wally.RomMaster.Persistence.SqlServer.Migrations
{
	public partial class DataFile : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<Guid>(
				name: "DataFileId",
				table: "[File]",
				type: "uniqueidentifier",
				nullable: true);

			migrationBuilder.CreateTable(
				name: "DataFile",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
					Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
					Category = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
					Version = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
					Date = table.Column<DateTime>(type: "datetime2", nullable: true),
					Author = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
					Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
					HomePage = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					Url = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DataFile", x => x.Id);
					table.ForeignKey(
						name: "FK_DataFile_[File]_FileId",
						column: x => x.FileId,
						principalTable: "[File]",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "Game",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
					Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
					Year = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
					DataFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Game", x => x.Id);
					table.ForeignKey(
						name: "FK_Game_DataFile_DataFileId",
						column: x => x.DataFileId,
						principalTable: "DataFile",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Rom",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
					Size = table.Column<long>(type: "bigint", nullable: false),
					Crc = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
					Sha1 = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
					Md5 = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
					GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Rom", x => x.Id);
					table.ForeignKey(
						name: "FK_Rom_Game_GameId",
						column: x => x.GameId,
						principalTable: "Game",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(name: "IX_DataFile_FileId", table: "DataFile", column: "FileId", unique: true);

			migrationBuilder.CreateIndex(name: "IX_Game_DataFileId", table: "Game", column: "DataFileId");

			migrationBuilder.CreateIndex(name: "IX_Rom_GameId", table: "Rom", column: "GameId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(name: "Rom");

			migrationBuilder.DropTable(name: "Game");

			migrationBuilder.DropTable(name: "DataFile");

			migrationBuilder.DropColumn(name: "DataFileId", table: "[File]");
		}
	}
}
