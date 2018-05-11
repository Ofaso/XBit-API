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
    [Migration("20180511180315_AddCoinAndMiner")]
    partial class AddCoinAndMiner
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("XBitApi.Models.Algorithm", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Algorithms");
                });

            modelBuilder.Entity("XBitApi.Models.Coin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("PriceUrl");

                    b.Property<string>("Website");

                    b.HasKey("Id");

                    b.ToTable("Coins");
                });

            modelBuilder.Entity("XBitApi.Models.CoinAlgorithm", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AlgorithmId");

                    b.Property<Guid>("CoinId");

                    b.HasKey("Id");

                    b.HasIndex("AlgorithmId");

                    b.HasIndex("CoinId");

                    b.ToTable("CoinAlgorithms");
                });

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

            modelBuilder.Entity("XBitApi.Models.Miner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CoinAlgorithmId");

                    b.Property<Guid>("MinerTypeId");

                    b.HasKey("Id");

                    b.HasIndex("CoinAlgorithmId");

                    b.HasIndex("MinerTypeId");

                    b.ToTable("Miners");
                });

            modelBuilder.Entity("XBitApi.Models.MinerAlgorithm", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AlgorithmId");

                    b.Property<double>("Hashrate");

                    b.Property<Guid>("MinerTypeId");

                    b.HasKey("Id");

                    b.HasIndex("AlgorithmId");

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

            modelBuilder.Entity("XBitApi.Models.CoinAlgorithm", b =>
                {
                    b.HasOne("XBitApi.Models.Algorithm", "Algorithm")
                        .WithMany()
                        .HasForeignKey("AlgorithmId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("XBitApi.Models.Coin", "Coin")
                        .WithMany("CoinAlgorithms")
                        .HasForeignKey("CoinId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XBitApi.Models.Miner", b =>
                {
                    b.HasOne("XBitApi.Models.CoinAlgorithm", "CoinAlgorithm")
                        .WithMany("Miners")
                        .HasForeignKey("CoinAlgorithmId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("XBitApi.Models.MinerType", "MinerType")
                        .WithMany("Miners")
                        .HasForeignKey("MinerTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XBitApi.Models.MinerAlgorithm", b =>
                {
                    b.HasOne("XBitApi.Models.Algorithm", "Algorithm")
                        .WithMany("MinerAlgorithms")
                        .HasForeignKey("AlgorithmId")
                        .OnDelete(DeleteBehavior.Cascade);

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
