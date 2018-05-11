﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using XBitApi.EF;

namespace XBitApi.Migrations
{
    [DbContext(typeof(XBitContext))]
    [Migration("20180511174326_AddMinerAlgorithm")]
    partial class AddMinerAlgorithm
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("XBitApi.Models.Manufacturer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<string>("Website");

                    b.HasKey("Id");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("XBitApi.Models.MinerAlgorithm", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Hashrate");

                    b.Property<Guid>("MinerTypeId");

                    b.HasKey("Id");

                    b.HasIndex("MinerTypeId");

                    b.ToTable("MinerAlgorithms");
                });

            modelBuilder.Entity("XBitApi.Models.MinerType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ManufacturerId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("MinerTypes");
                });

            modelBuilder.Entity("XBitApi.Models.MinerAlgorithm", b =>
                {
                    b.HasOne("XBitApi.Models.MinerType", "MinerType")
                        .WithMany("MinerAlgorithms")
                        .HasForeignKey("MinerTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XBitApi.Models.MinerType", b =>
                {
                    b.HasOne("XBitApi.Models.Manufacturer", "Manufacturer")
                        .WithMany("MinerTypes")
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
