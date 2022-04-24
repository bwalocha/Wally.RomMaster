﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wally.RomMaster.Persistence;

#nullable disable

namespace Wally.RomMaster.Persistence.SqlServer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220424130937_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Wally.RomMaster.Domain.Files.File", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Attributes")
                        .HasColumnType("int");

                    b.Property<string>("Crc")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime>("CreationTimeUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastAccessTimeUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastWriteTimeUtc")
                        .HasColumnType("datetime2");

                    b.Property<long>("Length")
                        .HasColumnType("bigint");

                    b.Property<string>("Md5")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Sha1")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("File");
                });

            modelBuilder.Entity("Wally.RomMaster.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("Wally.RomMaster.Domain.Files.File", b =>
                {
                    b.OwnsOne("Wally.RomMaster.Domain.Files.FileLocation", "Location", b1 =>
                        {
                            b1.Property<Guid>("FileId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Location")
                                .IsRequired()
                                .HasMaxLength(3000)
                                .HasColumnType("nvarchar(3000)")
                                .HasColumnName("Location");

                            b1.HasKey("FileId");

                            b1.HasIndex("Location")
                                .IsUnique();

                            b1.ToTable("File");

                            b1.WithOwner()
                                .HasForeignKey("FileId");
                        });

                    b.Navigation("Location")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
