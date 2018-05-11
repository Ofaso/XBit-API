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
    partial class XBitContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("XBitApi.Models.Balance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CoinId");

                    b.Property<Guid>("MiningFarmId");

                    b.Property<double>("TotalFarmedAmount");

                    b.Property<string>("WalletAddress");

                    b.HasKey("Id");

                    b.HasIndex("CoinId");

                    b.HasIndex("MiningFarmId");

                    b.ToTable("Balances");
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

            modelBuilder.Entity("XBitApi.Models.HostingPeriod", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDate");

                    b.Property<Guid>("MinerId");

                    b.Property<double>("PricePerMonth");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("MinerId");

                    b.ToTable("HostingPeriods");
                });

            modelBuilder.Entity("XBitApi.Models.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Locations");
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

                    b.Property<Guid>("MiningFarmId");

                    b.Property<Guid>("ShelfId");

                    b.HasKey("Id");

                    b.HasIndex("CoinAlgorithmId");

                    b.HasIndex("MinerTypeId");

                    b.HasIndex("MiningFarmId");

                    b.HasIndex("ShelfId");

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

            modelBuilder.Entity("XBitApi.Models.MiningFarm", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("MiningFarms");
                });

            modelBuilder.Entity("XBitApi.Models.Shelf", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("LocationId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Shelves");
                });

            modelBuilder.Entity("XBitApi.Models.Balance", b =>
                {
                    b.HasOne("XBitApi.Models.Coin", "Coin")
                        .WithMany("Balances")
                        .HasForeignKey("CoinId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("XBitApi.Models.MiningFarm", "MiningFarm")
                        .WithMany("Balances")
                        .HasForeignKey("MiningFarmId")
                        .OnDelete(DeleteBehavior.Cascade);
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

            modelBuilder.Entity("XBitApi.Models.HostingPeriod", b =>
                {
                    b.HasOne("XBitApi.Models.Miner", "Miner")
                        .WithMany("HostingPeriods")
                        .HasForeignKey("MinerId")
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

                    b.HasOne("XBitApi.Models.MiningFarm", "MiningFarm")
                        .WithMany("Miners")
                        .HasForeignKey("MiningFarmId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("XBitApi.Models.Shelf", "Shelf")
                        .WithMany("Miners")
                        .HasForeignKey("ShelfId")
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

            modelBuilder.Entity("XBitApi.Models.Shelf", b =>
                {
                    b.HasOne("XBitApi.Models.Location", "Location")
                        .WithMany("Shelves")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
