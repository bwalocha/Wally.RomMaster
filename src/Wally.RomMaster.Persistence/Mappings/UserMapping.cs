using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Wally.RomMaster.Domain.Users;

namespace Wally.RomMaster.Persistence.Mappings;

internal class UserMapping : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasIndex(a => a.Name)
			.IsUnique();

		builder.Property(user => user.Name)
			.IsRequired()
			.HasMaxLength(250);
	}
}
