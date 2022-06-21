using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Wally.RomMaster.Domain.DataFiles;

namespace Wally.RomMaster.Persistence.Mappings;

internal class RomMapping : IEntityTypeConfiguration<Rom>
{
	public void Configure(EntityTypeBuilder<Rom> builder)
	{
		builder.ToTable("Rom");

		builder.Property(a => a.Crc)
			.IsRequired()
			.HasMaxLength(8);
		builder.Property(a => a.Md5)
			.IsRequired(false)
			.HasMaxLength(64);
		builder.Property(a => a.Name)
			.IsRequired()
			.HasMaxLength(256);
		builder.Property(a => a.Sha1)
			.IsRequired(false)
			.HasMaxLength(64);
		builder.Property(a => a.Size)
			.IsRequired();

		// builder.HasIndex(a => new { a.Crc, a.Length }).IsUnique(false); // Length 0 files has the same Crc
	}
}
