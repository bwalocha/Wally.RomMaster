using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Wally.RomMaster.Domain.DataFiles;
using Wally.RomMaster.Domain.Files;

namespace Wally.RomMaster.Persistence.Mappings;

internal class DataFileMapping : IEntityTypeConfiguration<DataFile>
{
	public void Configure(EntityTypeBuilder<DataFile> builder)
	{
		builder.ToTable("DataFile");

		builder.Property(a => a.Author)
			.IsRequired(false)
			.HasMaxLength(512);
		builder.Property(a => a.Category)
			.IsRequired(false)
			.HasMaxLength(512);
		builder.Property(a => a.Date)
			.IsRequired(false);
		builder.Property(a => a.Description)
			.IsRequired(false)
			.HasMaxLength(512);
		builder.Property(a => a.Email)
			.IsRequired(false)
			.HasMaxLength(128);
		builder.Property(a => a.Name)
			.IsRequired()
			.HasMaxLength(256);
		builder.Property(a => a.Url)
			.IsRequired(false)
			.HasMaxLength(256);
		builder.Property(a => a.Version)
			.IsRequired(false)
			.HasMaxLength(128);
		builder.Property(a => a.HomePage)
			.IsRequired(false)
			.HasMaxLength(256);

		builder.HasOne(a => a.File)
			.WithOne(a => a.DataFile)
			.HasForeignKey<File>(a => a.DataFileId)
			.IsRequired(false);

		builder.HasMany(a => a.Games)
			.WithOne()
			.IsRequired();

		// builder.HasIndex(a => new { a.Crc, a.Length }).IsUnique(false); // Length 0 files has the same Crc
	}
}
