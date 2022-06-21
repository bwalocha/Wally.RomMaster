using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Wally.RomMaster.Domain.DataFiles;

namespace Wally.RomMaster.Persistence.Mappings;

internal class GameMapping : IEntityTypeConfiguration<Game>
{
	public void Configure(EntityTypeBuilder<Game> builder)
	{
		builder.ToTable("Game");

		builder.Property(a => a.Description)
			.IsRequired(false)
			.HasMaxLength(512);
		builder.Property(a => a.Name)
			.IsRequired()
			.HasMaxLength(512);
		builder.Property(a => a.Year)
			.IsRequired(false)
			.HasMaxLength(8);

		builder.HasMany(a => a.Roms)
			.WithOne()
			.IsRequired();

		// builder.HasIndex(a => new { a.Crc, a.Length }).IsUnique(false); // Length 0 files has the same Crc
	}
}
