﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

namespace Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210703210428_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("Domain.ItemDetail", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("examine")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<long>("highalch")
                        .HasColumnType("INTEGER");

                    b.Property<string>("icon")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<long>("limit")
                        .HasColumnType("INTEGER");

                    b.Property<long>("lowalch")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("members")
                        .HasColumnType("INTEGER");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<long>("value")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("ItemDetails");
                });

            modelBuilder.Entity("Domain.ItemHistorical", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("ItemHistoricals");
                });

            modelBuilder.Entity("Domain.ItemPriceSnapshot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(70)
                        .HasColumnType("TEXT");

                    b.Property<long?>("ItemHistoricalId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("high")
                        .HasColumnType("INTEGER");

                    b.Property<long>("highTime")
                        .HasColumnType("INTEGER");

                    b.Property<long>("low")
                        .HasColumnType("INTEGER");

                    b.Property<long>("lowTime")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ItemHistoricalId");

                    b.ToTable("ItemPriceSnapshots");
                });

            modelBuilder.Entity("Domain.ItemPriceSnapshot", b =>
                {
                    b.HasOne("Domain.ItemHistorical", null)
                        .WithMany("historical")
                        .HasForeignKey("ItemHistoricalId");
                });

            modelBuilder.Entity("Domain.ItemHistorical", b =>
                {
                    b.Navigation("historical");
                });
#pragma warning restore 612, 618
        }
    }
}
