using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Wally.RomMaster.Domain.Files;

namespace Wally.RomMaster.Persistence.Mappings;

internal class PathMapping : IEntityTypeConfiguration<Path>
{
	public void Configure(EntityTypeBuilder<Path> builder)
	{
		builder.HasIndex(a => a.Name)
			.IsUnique();

		builder.Property(a => a.Name)
			.IsRequired()
			.HasMaxLength(2048);

		builder.HasOne(a => a.Parent);
		builder.HasMany(a => a.Files)
			.WithOne(a => a.Path);
	}
}
