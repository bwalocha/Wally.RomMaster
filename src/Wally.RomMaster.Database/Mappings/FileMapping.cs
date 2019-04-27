﻿namespace Wally.RomMaster.Database.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Wally.RomMaster.Domain.Models;

    internal class FileMapping : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.Property(a => a.Path).IsRequired();
            builder.Property(a => a.Size).IsRequired();
            builder.Property(a => a.Crc).IsRequired(false);
            builder.Property(a => a.Md5).IsRequired(false);
            builder.Property(a => a.Sha1).IsRequired(false);

            builder.HasIndex(a => a.Path).IsUnique(true);
            builder.HasIndex(a => new { a.Crc, a.Size }).IsUnique(true); // TODO test it
        }
    }
}
