using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Wally.RomMaster.Domain.DataFiles;
using Wally.RomMaster.Domain.Files;

namespace Wally.RomMaster.Persistence.Mappings;

internal class FileMapping : IEntityTypeConfiguration<File>
{
	public void Configure(EntityTypeBuilder<File> builder)
	{
		builder.ToTable("[File]");

		builder.OwnsOne(
			a => a.Location,
			b =>
			{
				b.Property(c => c.Location)
					.IsRequired()
					.HasConversion(a => a.LocalPath, b => new Uri(b))
					.HasColumnName(nameof(File.Location))
					.HasMaxLength(3000);
				b.HasIndex(c => c.Location)
					.IsUnique();
			});

		builder.Property(a => a.Length)
			.IsRequired();
		builder.Property(a => a.Crc)
			.IsRequired()
			.HasMaxLength(16);
		builder.Property(a => a.Md5)
			.IsRequired(false)
			.HasMaxLength(32);
		builder.Property(a => a.Sha1)
			.IsRequired(false)
			.HasMaxLength(40);

		builder.HasOne(a => a.DataFile)
			.WithOne(a => a.File)
			.HasForeignKey<DataFile>(a => a.FileId)
			.IsRequired(false);

		// builder.HasIndex(a => new { a.Crc, a.Length }).IsUnique(false); // Length 0 files has the same Crc
	}
}
