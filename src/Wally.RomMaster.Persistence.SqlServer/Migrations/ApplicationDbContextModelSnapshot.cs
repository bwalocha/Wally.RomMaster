﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wally.RomMaster.Persistence;

#nullable disable

namespace Wally.RomMaster.Persistence.SqlServer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Wally.RomMaster.Domain.DataFiles.DataFile", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Category")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Email")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<Guid>("FileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("HomePage")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Url")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Version")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("FileId")
                        .IsUnique();

                    b.ToTable("DataFile", (string)null);
                });

            modelBuilder.Entity("Wally.RomMaster.Domain.DataFiles.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DataFileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Year")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.HasKey("Id");

                    b.HasIndex("DataFileId");

                    b.ToTable("Game", (string)null);
                });

            modelBuilder.Entity("Wally.RomMaster.Domain.DataFiles.Rom", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Crc")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Md5")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Sha1")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<decimal>("Size")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("Id");

                    b.HasIndex("Crc");

                    b.HasIndex("GameId");

                    b.ToTable("Rom", (string)null);
                });

            modelBuilder.Entity("Wally.RomMaster.Domain.Files.File", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Attributes")
                        .HasColumnType("int");

                    b.Property<string>("Crc")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<DateTime>("CreationTimeUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DataFileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastAccessTimeUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastWriteTimeUtc")
                        .HasColumnType("datetime2");

                    b.Property<long>("Length")
                        .HasColumnType("bigint");

                    b.Property<string>("Md5")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Sha1")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.HasIndex("Crc");

                    b.ToTable("[File]", (string)null);
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

            modelBuilder.Entity("Wally.RomMaster.Domain.DataFiles.DataFile", b =>
                {
                    b.HasOne("Wally.RomMaster.Domain.Files.File", "File")
                        .WithOne("DataFile")
                        .HasForeignKey("Wally.RomMaster.Domain.DataFiles.DataFile", "FileId");

                    b.Navigation("File");
                });

            modelBuilder.Entity("Wally.RomMaster.Domain.DataFiles.Game", b =>
                {
                    b.HasOne("Wally.RomMaster.Domain.DataFiles.DataFile", null)
                        .WithMany("Games")
                        .HasForeignKey("DataFileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Wally.RomMaster.Domain.DataFiles.Rom", b =>
                {
                    b.HasOne("Wally.RomMaster.Domain.DataFiles.Game", null)
                        .WithMany("Roms")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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

                            b1.ToTable("[File]");

                            b1.WithOwner()
                                .HasForeignKey("FileId");
                        });

                    b.Navigation("Location")
                        .IsRequired();
                });

            modelBuilder.Entity("Wally.RomMaster.Domain.DataFiles.DataFile", b =>
                {
                    b.Navigation("Games");
                });

            modelBuilder.Entity("Wally.RomMaster.Domain.DataFiles.Game", b =>
                {
                    b.Navigation("Roms");
                });

            modelBuilder.Entity("Wally.RomMaster.Domain.Files.File", b =>
                {
                    b.Navigation("DataFile");
                });
#pragma warning restore 612, 618
        }
    }
}
