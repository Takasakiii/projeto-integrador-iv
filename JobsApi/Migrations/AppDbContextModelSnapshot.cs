﻿// <auto-generated />
using System;
using JobsApi.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JobsApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<uint?>("ExpectedValue")
                        .HasColumnType("int unsigned");

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

                    b.Property<string>("Role")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("ImageId");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("JobsApi.Models.UserSkillModel", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<uint>("SkillId")
                        .HasColumnType("int unsigned");

                    b.Property<uint>("UserId")
                        .HasColumnType("int unsigned");

                    b.Property<uint>("Years")
                        .HasColumnType("int unsigned");

                    b.HasKey("Id");

                    b.HasIndex("SkillId");

                    b.HasIndex("UserId", "SkillId")
                        .IsUnique();

                    b.ToTable("UsersSkills", (string)null);
                });

            modelBuilder.Entity("JobsApi.Models.WorkModel", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTimeOffset?>("EndAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("StartAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTimeOffset(new DateTime(2023, 5, 8, 13, 8, 20, 989, DateTimeKind.Unspecified).AddTicks(3241), new TimeSpan(0, 0, 0, 0, 0)));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<uint>("UserId")
                        .HasColumnType("int unsigned");

                    b.Property<uint>("Value")
                        .HasColumnType("int unsigned");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Works", (string)null);
                });

            modelBuilder.Entity("JobsApi.Models.WorkSkillModel", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int unsigned");

                    b.Property<uint>("SkillId")
                        .HasColumnType("int unsigned");

                    b.Property<uint>("WorkId")
                        .HasColumnType("int unsigned");

                    b.HasKey("Id");

                    b.HasIndex("SkillId");

                    b.HasIndex("WorkId", "SkillId")
                        .IsUnique();

                    b.ToTable("WorksSkills", (string)null);
                });

            modelBuilder.Entity("JobsApi.Models.UserModel", b =>
                {
                    b.HasOne("JobsApi.Models.ImageModel", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Image");
                });

            modelBuilder.Entity("JobsApi.Models.UserSkillModel", b =>
                {
                    b.HasOne("JobsApi.Models.SkillModel", "Skill")
                        .WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("JobsApi.Models.UserModel", "User")
                        .WithMany("Skills")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Skill");

                    b.Navigation("User");
                });

            modelBuilder.Entity("JobsApi.Models.WorkModel", b =>
                {
                    b.HasOne("JobsApi.Models.UserModel", "User")
                        .WithMany("Works")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("JobsApi.Models.WorkSkillModel", b =>
                {
                    b.HasOne("JobsApi.Models.SkillModel", "Skill")
                        .WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("JobsApi.Models.WorkModel", "Work")
                        .WithMany("Skills")
                        .HasForeignKey("WorkId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Skill");

                    b.Navigation("Work");
                });

            modelBuilder.Entity("JobsApi.Models.UserModel", b =>
                {
                    b.Navigation("Skills");

                    b.Navigation("Works");
                });

            modelBuilder.Entity("JobsApi.Models.WorkModel", b =>
                {
                    b.Navigation("Skills");
                });
#pragma warning restore 612, 618
        }
    }
}
