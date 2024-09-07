﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShrimpPond.Persistence.DatabaseContext;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    [DbContext(typeof(ShrimpPondDbContext))]
    [Migration("20240907150245_test")]
    partial class test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ShrimpPond.Domain.Environments.EnvironmentStatus", b =>
                {
                    b.Property<int>("EnvironmentStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EnvironmentStatusId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PondId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EnvironmentStatusId");

                    b.HasIndex("PondId");

                    b.ToTable("EnvironmentStatus");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Certificate", b =>
                {
                    b.Property<int>("CertificateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CertificateId"), 1L, 1);

                    b.Property<string>("CertificateName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("FileData")
                        .HasColumnType("VARBINARY(MAX)");

                    b.Property<int?>("HarvestId")
                        .HasColumnType("int");

                    b.Property<string>("PondId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CertificateId");

                    b.HasIndex("HarvestId");

                    b.HasIndex("PondId");

                    b.ToTable("Certificate");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Feeding.Food.Food", b =>
                {
                    b.Property<int>("FoodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FoodId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FoodId");

                    b.ToTable("Food");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Feeding.Food.FoodFeeding", b =>
                {
                    b.Property<int>("FoodFeedingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FoodFeedingId"), 1L, 1);

                    b.Property<DateTime>("FeedingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PondId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("FoodFeedingId");

                    b.HasIndex("PondId");

                    b.ToTable("FoodFeeding");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Feeding.Food.FoodForFeeding", b =>
                {
                    b.Property<int>("FoodForFeedingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FoodForFeedingId"), 1L, 1);

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<int>("FoodFeedingId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FoodForFeedingId");

                    b.HasIndex("FoodFeedingId");

                    b.ToTable("FoodForFeeding");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Feeding.Medicine.Medicine", b =>
                {
                    b.Property<int>("MedicineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MedicineId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MedicineId");

                    b.ToTable("Medicine");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Feeding.Medicine.MedicineFeeding", b =>
                {
                    b.Property<int>("MedicineFeedingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MedicineFeedingId"), 1L, 1);

                    b.Property<DateTime>("FeedingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PondId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MedicineFeedingId");

                    b.HasIndex("PondId");

                    b.ToTable("MedicineFeeding");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Feeding.Medicine.MedicineForFeeding", b =>
                {
                    b.Property<int>("MedicineForFeedingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MedicineForFeedingId"), 1L, 1);

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<int>("MedicineFeedingId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MedicineForFeedingId");

                    b.HasIndex("MedicineFeedingId");

                    b.ToTable("MedicineForFeeding");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Harvest.Harvest", b =>
                {
                    b.Property<int>("HarvestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HarvestId"), 1L, 1);

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<DateTime>("HarvestDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("HarvestTime")
                        .HasColumnType("int");

                    b.Property<int>("HarvestType")
                        .HasColumnType("int");

                    b.Property<string>("PondId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("Size")
                        .HasColumnType("real");

                    b.HasKey("HarvestId");

                    b.HasIndex("PondId");

                    b.ToTable("Harvest");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.LossShrimp", b =>
                {
                    b.Property<int>("LossShrimpId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LossShrimpId"), 1L, 1);

                    b.Property<float>("LossValue")
                        .HasColumnType("real");

                    b.Property<string>("PondId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("LossShrimpId");

                    b.HasIndex("PondId");

                    b.ToTable("LossShrimp");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Pond", b =>
                {
                    b.Property<string>("PondId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("AmountShrimp")
                        .HasColumnType("real");

                    b.Property<float>("Deep")
                        .HasColumnType("real");

                    b.Property<float>("Diameter")
                        .HasColumnType("real");

                    b.Property<string>("OriginPondId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PondTypeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PondTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SeedId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("PondId");

                    b.HasIndex("PondTypeId");

                    b.ToTable("Pond");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.PondType", b =>
                {
                    b.Property<string>("PondTypeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PondTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PondTypeId");

                    b.ToTable("PondType");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.SizeShrimp", b =>
                {
                    b.Property<int>("SizeShrimpId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SizeShrimpId"), 1L, 1);

                    b.Property<string>("PondId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("SizeValue")
                        .HasColumnType("real");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("SizeShrimpId");

                    b.HasIndex("PondId");

                    b.ToTable("SizeShrimp");
                });

            modelBuilder.Entity("ShrimpPond.Domain.Environments.EnvironmentStatus", b =>
                {
                    b.HasOne("ShrimpPond.Domain.PondData.Pond", null)
                        .WithMany("EnvironmentStatus")
                        .HasForeignKey("PondId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Certificate", b =>
                {
                    b.HasOne("ShrimpPond.Domain.PondData.Harvest.Harvest", null)
                        .WithMany("Certificates")
                        .HasForeignKey("HarvestId");

                    b.HasOne("ShrimpPond.Domain.PondData.Pond", "Pond")
                        .WithMany("Certificates")
                        .HasForeignKey("PondId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pond");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Feeding.Food.FoodFeeding", b =>
                {
                    b.HasOne("ShrimpPond.Domain.PondData.Pond", "Pond")
                        .WithMany("FoodFeedings")
                        .HasForeignKey("PondId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pond");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Feeding.Food.FoodForFeeding", b =>
                {
                    b.HasOne("ShrimpPond.Domain.PondData.Feeding.Food.FoodFeeding", "FoodFeeding")
                        .WithMany("Foods")
                        .HasForeignKey("FoodFeedingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodFeeding");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Feeding.Medicine.MedicineFeeding", b =>
                {
                    b.HasOne("ShrimpPond.Domain.PondData.Pond", "Pond")
                        .WithMany("MedicineFeedings")
                        .HasForeignKey("PondId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pond");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Feeding.Medicine.MedicineForFeeding", b =>
                {
                    b.HasOne("ShrimpPond.Domain.PondData.Feeding.Medicine.MedicineFeeding", "MedicineFeeding")
                        .WithMany("Medicines")
                        .HasForeignKey("MedicineFeedingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MedicineFeeding");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Harvest.Harvest", b =>
                {
                    b.HasOne("ShrimpPond.Domain.PondData.Pond", "Pond")
                        .WithMany("Harvests")
                        .HasForeignKey("PondId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pond");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.LossShrimp", b =>
                {
                    b.HasOne("ShrimpPond.Domain.PondData.Pond", "Pond")
                        .WithMany("LossShrimps")
                        .HasForeignKey("PondId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pond");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Pond", b =>
                {
                    b.HasOne("ShrimpPond.Domain.PondData.PondType", "PondType")
                        .WithMany()
                        .HasForeignKey("PondTypeId");

                    b.Navigation("PondType");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.SizeShrimp", b =>
                {
                    b.HasOne("ShrimpPond.Domain.PondData.Pond", "Pond")
                        .WithMany("SizeShrimps")
                        .HasForeignKey("PondId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pond");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Feeding.Food.FoodFeeding", b =>
                {
                    b.Navigation("Foods");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Feeding.Medicine.MedicineFeeding", b =>
                {
                    b.Navigation("Medicines");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Harvest.Harvest", b =>
                {
                    b.Navigation("Certificates");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Pond", b =>
                {
                    b.Navigation("Certificates");

                    b.Navigation("EnvironmentStatus");

                    b.Navigation("FoodFeedings");

                    b.Navigation("Harvests");

                    b.Navigation("LossShrimps");

                    b.Navigation("MedicineFeedings");

                    b.Navigation("SizeShrimps");
                });
#pragma warning restore 612, 618
        }
    }
}
