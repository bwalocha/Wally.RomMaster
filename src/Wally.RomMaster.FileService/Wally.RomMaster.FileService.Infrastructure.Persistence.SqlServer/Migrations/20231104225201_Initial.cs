using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wally.RomMaster.FileService.Infrastructure.Persistence.SqlServer.Migrations
{
	/// <inheritdoc />
	public partial class Initial : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Path",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
					Name = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
					CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
					ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
					Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
					CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
					ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
				},
				constraints: table => { table.PrimaryKey("PK_User", x => x.Id); });

			migrationBuilder.CreateTable(
				name: "\"File\"",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Location = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
					Length = table.Column<long>(type: "bigint", nullable: false),
					Crc32 = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
					Attributes = table.Column<int>(type: "int", nullable: false),
					CreationTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
					LastAccessTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
					LastWriteTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
					PathId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
					CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ModifiedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
					ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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

			migrationBuilder.CreateIndex(name: "IX_\"File\"_Crc32", table: "\"File\"", column: "Crc32");

			migrationBuilder.CreateIndex(
				name: "IX_\"File\"_Location",
				table: "\"File\"",
				column: "Location",
				unique: true);

			migrationBuilder.CreateIndex(name: "IX_\"File\"_PathId", table: "\"File\"", column: "PathId");

			migrationBuilder.CreateIndex(name: "IX_Path_Name", table: "Path", column: "Name", unique: true);

			migrationBuilder.CreateIndex(name: "IX_Path_ParentId", table: "Path", column: "ParentId");

			migrationBuilder.CreateIndex(name: "IX_User_Name", table: "User", column: "Name", unique: true);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(name: "\"File\"");

			migrationBuilder.DropTable(name: "User");

			migrationBuilder.DropTable(name: "Path");
		}
	}
}
