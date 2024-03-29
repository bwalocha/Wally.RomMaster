﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wally.RomMaster.FileService.Infrastructure.Persistence;

#nullable disable

namespace Wally.RomMaster.FileService.Infrastructure.Persistence.SQLite.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("Wally.RomMaster.FileService.Domain.Files.File", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("Attributes")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Crc32")
                        .HasMaxLength(8)
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreationTimeUtc")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastAccessTimeUtc")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastWriteTimeUtc")
                        .HasColumnType("TEXT");

                    b.Property<long>("Length")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PathId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Crc32");

                    b.HasIndex("PathId");

                    b.ToTable("\"File\"", (string)null);
                });

            modelBuilder.Entity("Wally.RomMaster.FileService.Domain.Files.Path", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ParentId");

                    b.ToTable("Path");
                });

            modelBuilder.Entity("Wally.RomMaster.FileService.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset?>("ModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("Wally.RomMaster.FileService.Domain.Files.File", b =>
                {
                    b.HasOne("Wally.RomMaster.FileService.Domain.Files.Path", "Path")
                        .WithMany("Files")
                        .HasForeignKey("PathId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Wally.RomMaster.FileService.Domain.Files.FileLocation", "Location", b1 =>
                        {
                            b1.Property<Guid>("FileId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Location")
                                .IsRequired()
                                .HasMaxLength(3000)
                                .HasColumnType("TEXT")
                                .HasColumnName("Location");

                            b1.HasKey("FileId");

                            b1.HasIndex("Location")
                                .IsUnique();

                            b1.ToTable("\"File\"");

                            b1.WithOwner()
                                .HasForeignKey("FileId");
                        });

                    b.Navigation("Location")
                        .IsRequired();

                    b.Navigation("Path");
                });

            modelBuilder.Entity("Wally.RomMaster.FileService.Domain.Files.Path", b =>
                {
                    b.HasOne("Wally.RomMaster.FileService.Domain.Files.Path", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Wally.RomMaster.FileService.Domain.Files.Path", b =>
                {
                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}
