using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wally.RomMaster.HashService.Domain.Users;

namespace Wally.RomMaster.HashService.Infrastructure.Persistence.Mappings;

internal class UserMapping : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasIndex(a => a.Name)
			.IsUnique();

		builder.Property(a => a.Name)
			.HasMaxLength(256);
	}
}
