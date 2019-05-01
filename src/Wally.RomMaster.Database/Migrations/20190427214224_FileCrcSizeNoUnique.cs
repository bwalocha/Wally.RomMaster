using Microsoft.EntityFrameworkCore.Migrations;

namespace Wally.RomMaster.Database.Migrations
{
    public partial class FileCrcSizeNoUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_File_Crc_Size",
                table: "File");

            migrationBuilder.CreateIndex(
                name: "IX_File_Crc_Size",
                table: "File",
                columns: new[] { "Crc", "Size" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_File_Crc_Size",
                table: "File");

            migrationBuilder.CreateIndex(
                name: "IX_File_Crc_Size",
                table: "File",
                columns: new[] { "Crc", "Size" },
                unique: true);
        }
    }
}
