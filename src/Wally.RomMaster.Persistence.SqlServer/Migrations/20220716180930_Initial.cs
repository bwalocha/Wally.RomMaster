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
				name: "Path",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
					Name = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Path", x => x.Id);
					table.ForeignKey(
						name: "FK_Path_Path_ParentId",
						column: x => x.ParentId,
						principalTable: "Path",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "User",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
				},
				constraints: table => { table.PrimaryKey("PK_User", x => x.Id); });

			migrationBuilder.CreateTable(
				name: "\"File\"",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Location = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
					Length = table.Column<long>(type: "bigint", nullable: false),
					Crc = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
					Sha1 = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
					Md5 = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
					Attributes = table.Column<int>(type: "int", nullable: false),
					CreationTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
					LastAccessTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
					LastWriteTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
					ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					PathId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					DataFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_\"File\"", x => x.Id);
					table.ForeignKey(
						name: "FK_\"File\"_Path_PathId",
						column: x => x.PathId,
						principalTable: "Path",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "DataFile",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
					Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
					Category = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
					Version = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
					Date = table.Column<DateTime>(type: "datetime2", nullable: true),
					Author = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
					Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
					HomePage = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					Url = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DataFile", x => x.Id);
					table.ForeignKey(
						name: "FK_DataFile_\"File\"_FileId",
						column: x => x.FileId,
						principalTable: "\"File\"",
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
					Size = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
					Crc = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
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

			migrationBuilder.CreateIndex(name: "IX_\"File\"_Crc", table: "\"File\"", column: "Crc");

			migrationBuilder.CreateIndex(
				name: "IX_\"File\"_Location",
				table: "\"File\"",
				column: "Location",
				unique: true);

			migrationBuilder.CreateIndex(name: "IX_\"File\"_PathId", table: "\"File\"", column: "PathId");

			migrationBuilder.CreateIndex(name: "IX_DataFile_FileId", table: "DataFile", column: "FileId", unique: true);

			migrationBuilder.CreateIndex(name: "IX_Game_DataFileId", table: "Game", column: "DataFileId");

			migrationBuilder.CreateIndex(name: "IX_Path_Name", table: "Path", column: "Name", unique: true);

			migrationBuilder.CreateIndex(name: "IX_Path_ParentId", table: "Path", column: "ParentId");

			migrationBuilder.CreateIndex(name: "IX_Rom_Crc", table: "Rom", column: "Crc");

			migrationBuilder.CreateIndex(name: "IX_Rom_GameId", table: "Rom", column: "GameId");

			migrationBuilder.CreateIndex(name: "IX_User_Name", table: "User", column: "Name", unique: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(name: "Rom");

			migrationBuilder.DropTable(name: "User");

			migrationBuilder.DropTable(name: "Game");

			migrationBuilder.DropTable(name: "DataFile");

			migrationBuilder.DropTable(name: "\"File\"");

			migrationBuilder.DropTable(name: "Path");
		}
	}
}
