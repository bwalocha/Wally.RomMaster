using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wally.RomMaster.FileService.Domain.Users;

namespace Wally.RomMaster.FileService.Infrastructure.Persistence.Mappings;

internal class UserMapping : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasIndex(a => a.Name)
			.IsUnique()
			.HasFilter($"{nameof(User.IsDeleted)} != 1");
		
		builder.Property(a => a.Name)
			.HasMaxLength(256);
	}
}
