﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wally.RomMaster.NotificationService.Infrastructure.Persistence;

#nullable disable

namespace Wally.RomMaster.NotificationService.Infrastructure.Persistence.MySql.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("NotificationService")
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Wally.RomMaster.NotificationService.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("IsDeleted != 1");

                    b.ToTable("User", "NotificationService");
                });
#pragma warning restore 612, 618
        }
    }
}