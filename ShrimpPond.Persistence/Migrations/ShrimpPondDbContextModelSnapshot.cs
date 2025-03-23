﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShrimpPond.Persistence.DatabaseContext;

#nullable disable

namespace ShrimpPond.Persistence.Migrations
{
    [DbContext(typeof(ShrimpPondDbContext))]
    partial class ShrimpPondDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.36")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ShrimpPond.Application.Feature.TimeSetting.Command.CreateTimeSetting.TimeSettingObject", b =>
                {
                    b.Property<int>("timeSettingObjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("timeSettingObjectId"), 1L, 1);

                    b.Property<int>("index")
                        .HasColumnType("int");

                    b.Property<string>("time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("timeSettingId")
                        .HasColumnType("int");

                    b.HasKey("timeSettingObjectId");

                    b.HasIndex("timeSettingId");

                    b.ToTable("timeSettingObjects");
                });

            modelBuilder.Entity("ShrimpPond.Domain.Environments.EnvironmentStatus", b =>
                {
                    b.Property<int>("environmentStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("environmentStatusId"), 1L, 1);

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pondId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("environmentStatusId");

                    b.HasIndex("pondId");

                    b.ToTable("EnvironmentStatus");
                });

            modelBuilder.Entity("ShrimpPond.Domain.Farm.Farm", b =>
                {
                    b.Property<int>("farmId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("farmId"), 1L, 1);

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("farmName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("farmId");

                    b.ToTable("Farms");
                });

            modelBuilder.Entity("ShrimpPond.Domain.Food.Food", b =>
                {
                    b.Property<int>("foodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("foodId"), 1L, 1);

                    b.Property<int>("farmId")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("foodId");

                    b.HasIndex("farmId");

                    b.ToTable("Food");
                });

            modelBuilder.Entity("ShrimpPond.Domain.Food.FoodFeeding", b =>
                {
                    b.Property<int>("foodFeedingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("foodFeedingId"), 1L, 1);

                    b.Property<DateTime>("feedingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("pondId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("foodFeedingId");

                    b.HasIndex("pondId");

                    b.ToTable("FoodFeeding");
                });

            modelBuilder.Entity("ShrimpPond.Domain.Food.FoodForFeeding", b =>
                {
                    b.Property<int>("foodForFeedingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("foodForFeedingId"), 1L, 1);

                    b.Property<float>("amount")
                        .HasColumnType("real");

                    b.Property<int>("foodFeedingId")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("foodForFeedingId");

                    b.HasIndex("foodFeedingId");

                    b.ToTable("FoodForFeeding");
                });

            modelBuilder.Entity("ShrimpPond.Domain.Medicine.Medicine", b =>
                {
                    b.Property<int>("medicineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("medicineId"), 1L, 1);

                    b.Property<int>("farmId")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("medicineId");

                    b.HasIndex("farmId");

                    b.ToTable("Medicine");
                });

            modelBuilder.Entity("ShrimpPond.Domain.Medicine.MedicineFeeding", b =>
                {
                    b.Property<int>("medicineFeedingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("medicineFeedingId"), 1L, 1);

                    b.Property<DateTime>("feedingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("pondId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("medicineFeedingId");

                    b.HasIndex("pondId");

                    b.ToTable("MedicineFeeding");
                });

            modelBuilder.Entity("ShrimpPond.Domain.Medicine.MedicineForFeeding", b =>
                {
                    b.Property<int>("medicineForFeedingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("medicineForFeedingId"), 1L, 1);

                    b.Property<float>("amount")
                        .HasColumnType("real");

                    b.Property<int>("medicineFeedingId")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("medicineForFeedingId");

                    b.HasIndex("medicineFeedingId");

                    b.ToTable("MedicineForFeeding");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Certificate", b =>
                {
                    b.Property<int>("certificateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("certificateId"), 1L, 1);

                    b.Property<string>("certificateName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("fileData")
                        .HasColumnType("VARBINARY(MAX)");

                    b.Property<int?>("harvestId")
                        .HasColumnType("int");

                    b.Property<string>("pondId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("certificateId");

                    b.HasIndex("harvestId");

                    b.HasIndex("pondId");

                    b.ToTable("Certificate");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.CleanSensor.CleanSensor", b =>
                {
                    b.Property<int>("cleanSensorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("cleanSensorId"), 1L, 1);

                    b.Property<DateTime>("cleanTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("farmId")
                        .HasColumnType("int");

                    b.HasKey("cleanSensorId");

                    b.HasIndex("farmId");

                    b.ToTable("CleanSensor");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Harvest.Harvest", b =>
                {
                    b.Property<int>("harvestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("harvestId"), 1L, 1);

                    b.Property<float>("amount")
                        .HasColumnType("real");

                    b.Property<int>("farmId")
                        .HasColumnType("int");

                    b.Property<DateTime>("harvestDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("harvestTime")
                        .HasColumnType("int");

                    b.Property<int>("harvestType")
                        .HasColumnType("int");

                    b.Property<string>("pondId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("seedId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("size")
                        .HasColumnType("real");

                    b.HasKey("harvestId");

                    b.HasIndex("farmId");

                    b.HasIndex("pondId");

                    b.ToTable("Harvests");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.LossShrimp", b =>
                {
                    b.Property<int>("lossShrimpId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("lossShrimpId"), 1L, 1);

                    b.Property<float>("lossValue")
                        .HasColumnType("real");

                    b.Property<string>("pondId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("updateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("lossShrimpId");

                    b.HasIndex("pondId");

                    b.ToTable("LossShrimp");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Pond", b =>
                {
                    b.Property<string>("pondId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("amountShrimp")
                        .HasColumnType("real");

                    b.Property<float>("deep")
                        .HasColumnType("real");

                    b.Property<float>("diameter")
                        .HasColumnType("real");

                    b.Property<int>("farmId")
                        .HasColumnType("int");

                    b.Property<string>("originPondId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pondName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pondTypeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("seedId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("seedName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("startDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("pondId");

                    b.HasIndex("pondTypeId");

                    b.ToTable("Pond");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.PondType", b =>
                {
                    b.Property<string>("pondTypeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("farmId")
                        .HasColumnType("int");

                    b.Property<string>("pondTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("pondTypeId");

                    b.HasIndex("farmId");

                    b.ToTable("PondType");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.SizeShrimp", b =>
                {
                    b.Property<int>("sizeShrimpId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("sizeShrimpId"), 1L, 1);

                    b.Property<string>("pondId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("sizeValue")
                        .HasColumnType("real");

                    b.Property<DateTime>("updateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("sizeShrimpId");

                    b.HasIndex("pondId");

                    b.ToTable("SizeShrimp");
                });

            modelBuilder.Entity("ShrimpPond.Domain.TimeSetting.TimeSetting", b =>
                {
                    b.Property<int>("timeSettingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("timeSettingId"), 1L, 1);

                    b.Property<bool>("enableFarm")
                        .HasColumnType("bit");

                    b.Property<int>("farmId")
                        .HasColumnType("int");

                    b.HasKey("timeSettingId");

                    b.HasIndex("farmId");

                    b.ToTable("TimeSettings");
                });

            modelBuilder.Entity("ShrimpPond.Application.Feature.TimeSetting.Command.CreateTimeSetting.TimeSettingObject", b =>
                {
                    b.HasOne("ShrimpPond.Domain.TimeSetting.TimeSetting", "timeSetting")
                        .WithMany("timeSettingObjects")
                        .HasForeignKey("timeSettingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("timeSetting");
                });

            modelBuilder.Entity("ShrimpPond.Domain.Environments.EnvironmentStatus", b =>
                {
                    b.HasOne("ShrimpPond.Domain.PondData.Pond", "pond")
                        .WithMany("environmentStatus")
                        .HasForeignKey("pondId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("pond");
                });

            modelBuilder.Entity("ShrimpPond.Domain.Food.Food", b =>
                {
                    b.HasOne("ShrimpPond.Domain.Farm.Farm", "farm")
                        .WithMany()
                        .HasForeignKey("farmId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("farm");
                });

            modelBuilder.Entity("ShrimpPond.Domain.Food.FoodFeeding", b =>
                {
                    b.HasOne("ShrimpPond.Domain.PondData.Pond", "pond")
                        .WithMany("foodFeedings")
                        .HasForeignKey("pondId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("pond");
                });

            modelBuilder.Entity("ShrimpPond.Domain.Food.FoodForFeeding", b =>
                {
                    b.HasOne("ShrimpPond.Domain.Food.FoodFeeding", "foodFeeding")
                        .WithMany("foods")
                        .HasForeignKey("foodFeedingId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("foodFeeding");
                });

            modelBuilder.Entity("ShrimpPond.Domain.Medicine.Medicine", b =>
                {
                    b.HasOne("ShrimpPond.Domain.Farm.Farm", "farm")
                        .WithMany()
                        .HasForeignKey("farmId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("farm");
                });

            modelBuilder.Entity("ShrimpPond.Domain.Medicine.MedicineFeeding", b =>
                {
                    b.HasOne("ShrimpPond.Domain.PondData.Pond", "pond")
                        .WithMany("medicineFeedings")
                        .HasForeignKey("pondId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("pond");
                });

            modelBuilder.Entity("ShrimpPond.Domain.Medicine.MedicineForFeeding", b =>
                {
                    b.HasOne("ShrimpPond.Domain.Medicine.MedicineFeeding", "medicineFeeding")
                        .WithMany("medicines")
                        .HasForeignKey("medicineFeedingId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("medicineFeeding");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Certificate", b =>
                {
                    b.HasOne("ShrimpPond.Domain.PondData.Harvest.Harvest", null)
                        .WithMany("certificates")
                        .HasForeignKey("harvestId");

                    b.HasOne("ShrimpPond.Domain.PondData.Pond", "pond")
                        .WithMany("certificates")
                        .HasForeignKey("pondId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("pond");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.CleanSensor.CleanSensor", b =>
                {
                    b.HasOne("ShrimpPond.Domain.Farm.Farm", "farm")
                        .WithMany()
                        .HasForeignKey("farmId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("farm");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Harvest.Harvest", b =>
                {
                    b.HasOne("ShrimpPond.Domain.Farm.Farm", "farm")
                        .WithMany()
                        .HasForeignKey("farmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShrimpPond.Domain.PondData.Pond", "pond")
                        .WithMany("harvests")
                        .HasForeignKey("pondId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("farm");

                    b.Navigation("pond");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.LossShrimp", b =>
                {
                    b.HasOne("ShrimpPond.Domain.PondData.Pond", "pond")
                        .WithMany("lossShrimps")
                        .HasForeignKey("pondId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("pond");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Pond", b =>
                {
                    b.HasOne("ShrimpPond.Domain.PondData.PondType", "pondType")
                        .WithMany()
                        .HasForeignKey("pondTypeId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("pondType");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.PondType", b =>
                {
                    b.HasOne("ShrimpPond.Domain.Farm.Farm", "farm")
                        .WithMany()
                        .HasForeignKey("farmId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("farm");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.SizeShrimp", b =>
                {
                    b.HasOne("ShrimpPond.Domain.PondData.Pond", "pond")
                        .WithMany("sizeShrimps")
                        .HasForeignKey("pondId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("pond");
                });

            modelBuilder.Entity("ShrimpPond.Domain.TimeSetting.TimeSetting", b =>
                {
                    b.HasOne("ShrimpPond.Domain.Farm.Farm", "farm")
                        .WithMany()
                        .HasForeignKey("farmId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("farm");
                });

            modelBuilder.Entity("ShrimpPond.Domain.Food.FoodFeeding", b =>
                {
                    b.Navigation("foods");
                });

            modelBuilder.Entity("ShrimpPond.Domain.Medicine.MedicineFeeding", b =>
                {
                    b.Navigation("medicines");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Harvest.Harvest", b =>
                {
                    b.Navigation("certificates");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Pond", b =>
                {
                    b.Navigation("certificates");

                    b.Navigation("environmentStatus");

                    b.Navigation("foodFeedings");

                    b.Navigation("harvests");

                    b.Navigation("lossShrimps");

                    b.Navigation("medicineFeedings");

                    b.Navigation("sizeShrimps");
                });

            modelBuilder.Entity("ShrimpPond.Domain.TimeSetting.TimeSetting", b =>
                {
                    b.Navigation("timeSettingObjects");
                });
#pragma warning restore 612, 618
        }
    }
}
