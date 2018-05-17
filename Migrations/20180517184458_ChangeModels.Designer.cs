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
    [Migration("20180517184458_ChangeModels")]
    partial class ChangeModels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("XBitApi.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CountryId");

                    b.Property<string>("Place");

                    b.Property<string>("Street");

                    b.Property<string>("Zip");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("XBitApi.Models.Administrator", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.Property<Guid>("UserInformationId");

                    b.HasKey("Id");

                    b.HasIndex("UserInformationId");

                    b.ToTable("Administrators");
                });

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

            modelBuilder.Entity("XBitApi.Models.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("XBitApi.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AddressId");

                    b.Property<string>("FarmMail");

                    b.Property<string>("Password");

                    b.Property<Guid>("UserInformationId");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("UserInformationId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("XBitApi.Models.FarmMember", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CustomerId");

                    b.Property<Guid>("FarmRightId");

                    b.Property<Guid>("MiningFarmId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("FarmRightId");

                    b.HasIndex("MiningFarmId");

                    b.ToTable("FarmMembers");
                });

            modelBuilder.Entity("XBitApi.Models.FarmRight", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CanBuyMiningPackages");

                    b.Property<bool>("CanSwitchCoins");

                    b.Property<bool>("CanWithdrawCoins");

                    b.Property<bool>("HasReadRights");

                    b.HasKey("Id");

                    b.ToTable("FarmRights");
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

                    b.Property<Guid>("AddressId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("XBitApi.Models.LocationAdministrator", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AdministratorId");

                    b.Property<Guid>("LocationId");

                    b.HasKey("Id");

                    b.HasIndex("AdministratorId");

                    b.HasIndex("LocationId");

                    b.ToTable("LocationAdministrators");
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

                    b.Property<Guid>("AdminCustomerId");

                    b.Property<Guid?>("CustomerId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

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

            modelBuilder.Entity("XBitApi.Models.UserInformation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<string>("Surname");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("UserInformations");
                });

            modelBuilder.Entity("XBitApi.Models.Address", b =>
                {
                    b.HasOne("XBitApi.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XBitApi.Models.Administrator", b =>
                {
                    b.HasOne("XBitApi.Models.UserInformation", "UserInformation")
                        .WithMany()
                        .HasForeignKey("UserInformationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XBitApi.Models.Balance", b =>
                {
                    b.HasOne("XBitApi.Models.Coin", "Coin")
                        .WithMany()
                        .HasForeignKey("CoinId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("XBitApi.Models.MiningFarm", "MiningFarm")
                        .WithMany()
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
                        .WithMany()
                        .HasForeignKey("CoinId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XBitApi.Models.Customer", b =>
                {
                    b.HasOne("XBitApi.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("XBitApi.Models.UserInformation", "UserInformation")
                        .WithMany()
                        .HasForeignKey("UserInformationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XBitApi.Models.FarmMember", b =>
                {
                    b.HasOne("XBitApi.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("XBitApi.Models.FarmRight", "FarmRight")
                        .WithMany()
                        .HasForeignKey("FarmRightId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("XBitApi.Models.MiningFarm", "MiningFarm")
                        .WithMany()
                        .HasForeignKey("MiningFarmId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XBitApi.Models.HostingPeriod", b =>
                {
                    b.HasOne("XBitApi.Models.Miner", "Miner")
                        .WithMany()
                        .HasForeignKey("MinerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XBitApi.Models.Location", b =>
                {
                    b.HasOne("XBitApi.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XBitApi.Models.LocationAdministrator", b =>
                {
                    b.HasOne("XBitApi.Models.Administrator", "Administrator")
                        .WithMany()
                        .HasForeignKey("AdministratorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("XBitApi.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XBitApi.Models.Miner", b =>
                {
                    b.HasOne("XBitApi.Models.CoinAlgorithm", "CoinAlgorithm")
                        .WithMany()
                        .HasForeignKey("CoinAlgorithmId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("XBitApi.Models.MinerType", "MinerType")
                        .WithMany()
                        .HasForeignKey("MinerTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("XBitApi.Models.MiningFarm", "MiningFarm")
                        .WithMany()
                        .HasForeignKey("MiningFarmId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("XBitApi.Models.Shelf", "Shelf")
                        .WithMany()
                        .HasForeignKey("ShelfId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XBitApi.Models.MinerAlgorithm", b =>
                {
                    b.HasOne("XBitApi.Models.Algorithm", "Algorithm")
                        .WithMany()
                        .HasForeignKey("AlgorithmId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("XBitApi.Models.MinerType", "MinerType")
                        .WithMany()
                        .HasForeignKey("MinerTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XBitApi.Models.MinerType", b =>
                {
                    b.HasOne("XBitApi.Models.Manufacturer", "Manufacturer")
                        .WithMany()
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XBitApi.Models.MiningFarm", b =>
                {
                    b.HasOne("XBitApi.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("XBitApi.Models.Shelf", b =>
                {
                    b.HasOne("XBitApi.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
