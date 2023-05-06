﻿// <auto-generated />
using System;
using JobsApi.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JobsApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230505235258_AddUserDescriptionAndExpectedValue")]
    partial class AddUserDescriptionAndExpectedValue
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("JobsApi.Models.ImageModel", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<byte[]>("Data")
                        .IsRequired()
                        .HasMaxLength(4194304)
                        .HasColumnType("longblob");

                    b.Property<string>("Mime")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Images", (string)null);
                });

            modelBuilder.Entity("JobsApi.Models.SkillModel", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Skills", (string)null);
                });

            modelBuilder.Entity("JobsApi.Models.UserModel", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("ExpectedValue")
                        .HasColumnType("int");

                    b.Property<string>("ImageId")
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(92)
                        .HasColumnType("varchar(92)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("ImageId");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("JobsApi.Models.UserModel", b =>
                {
                    b.HasOne("JobsApi.Models.ImageModel", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Image");
                });
#pragma warning restore 612, 618
        }
    }
}
