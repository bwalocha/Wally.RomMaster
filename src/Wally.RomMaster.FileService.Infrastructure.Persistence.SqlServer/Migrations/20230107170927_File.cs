using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wally.RomMaster.FileService.Persistence.SqlServer.Migrations
{
	/// <inheritdoc />
	public partial class File : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<DateTime>(
				name: "CreatedAt",
				table: "User",
				type: "datetime2",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

			migrationBuilder.AddColumn<Guid>(
				name: "CreatedById",
				table: "User",
				type: "uniqueidentifier",
				nullable: false,
				defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

			migrationBuilder.AddColumn<DateTime>(name: "ModifiedAt", table: "User", type: "datetime2", nullable: true);

			migrationBuilder.AddColumn<Guid>(
				name: "ModifiedById",
				table: "User",
				type: "uniqueidentifier",
				nullable: true);

			migrationBuilder.CreateTable(
				name: "Path",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
					Name = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
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
				name: "\"File\"",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Location = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
					Length = table.Column<long>(type: "bigint", nullable: false),
					Attributes = table.Column<int>(type: "int", nullable: false),
					CreationTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
					LastAccessTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
					LastWriteTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
					ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					PathId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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

			migrationBuilder.CreateIndex(
				name: "IX_\"File\"_Location",
				table: "\"File\"",
				column: "Location",
				unique: true);

			migrationBuilder.CreateIndex(name: "IX_\"File\"_PathId", table: "\"File\"", column: "PathId");

			migrationBuilder.CreateIndex(name: "IX_Path_Name", table: "Path", column: "Name", unique: true);

			migrationBuilder.CreateIndex(name: "IX_Path_ParentId", table: "Path", column: "ParentId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(name: "\"File\"");

			migrationBuilder.DropTable(name: "Path");

			migrationBuilder.DropColumn(name: "CreatedAt", table: "User");

			migrationBuilder.DropColumn(name: "CreatedById", table: "User");

			migrationBuilder.DropColumn(name: "ModifiedAt", table: "User");

			migrationBuilder.DropColumn(name: "ModifiedById", table: "User");
		}
	}
}
