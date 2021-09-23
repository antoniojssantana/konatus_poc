﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using konatus.data.Context;

namespace konatus.data.Migrations
{
    [DbContext(typeof(KonatusDbContext))]
    [Migration("20210923153718_AlterValue")]
    partial class AlterValue
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("konatus.business.Models.MaintenanceModel", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(150)");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValue(new DateTime(2021, 9, 23, 12, 37, 17, 849, DateTimeKind.Local).AddTicks(4151));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(250)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("StatusMaintenance")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Maintenances");
                });

            modelBuilder.Entity("konatus.business.Models.StageModel", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValue(new DateTime(2021, 9, 23, 12, 37, 17, 858, DateTimeKind.Local).AddTicks(7855));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("MaintenanceId")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("StatusStage")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(100000)
                        .HasColumnType("varchar(100000)");

                    b.HasKey("Id");

                    b.HasIndex("MaintenanceId");

                    b.ToTable("Stages");
                });

            modelBuilder.Entity("konatus.business.Models.StageModel", b =>
                {
                    b.HasOne("konatus.business.Models.MaintenanceModel", "Maintenance")
                        .WithMany()
                        .HasForeignKey("MaintenanceId")
                        .IsRequired();

                    b.Navigation("Maintenance");
                });
#pragma warning restore 612, 618
        }
    }
}
