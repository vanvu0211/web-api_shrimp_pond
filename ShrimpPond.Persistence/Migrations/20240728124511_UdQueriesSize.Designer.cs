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
    [Migration("20240728124511_UdQueriesSize")]
    partial class UdQueriesSize
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Certificate", b =>
                {
                    b.Property<int>("CertificateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CertificateId"), 1L, 1);

                    b.Property<string>("CertificateName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CollectId")
                        .HasColumnType("int");

                    b.Property<byte[]>("FileData")
                        .HasColumnType("VARBINARY(MAX)");

                    b.Property<string>("PondId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CertificateId");

                    b.HasIndex("CollectId");

                    b.HasIndex("PondId");

                    b.ToTable("Certificate");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Collect.Collect", b =>
                {
                    b.Property<int>("CollectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CollectId"), 1L, 1);

                    b.Property<float>("AmountShrimpCollect")
                        .HasColumnType("real");

                    b.Property<DateTime>("CollectDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CollectTime")
                        .HasColumnType("int");

                    b.Property<int>("CollectType")
                        .HasColumnType("int");

                    b.Property<string>("PondId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("SizeShrimpCollect")
                        .HasColumnType("real");

                    b.HasKey("CollectId");

                    b.HasIndex("PondId");

                    b.ToTable("Collect");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Collect.Pond", b =>
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
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FoodFeedingId");

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
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MedicineFeedingId");

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("LossShrimpId");

                    b.ToTable("LossShrimp");
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("SizeValue")
                        .HasColumnType("real");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("SizeShrimpId");

                    b.ToTable("SizeShrimp");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Certificate", b =>
                {
                    b.HasOne("ShrimpPond.Domain.PondData.Collect.Collect", null)
                        .WithMany("Certificates")
                        .HasForeignKey("CollectId");

                    b.HasOne("ShrimpPond.Domain.PondData.Collect.Pond", "Pond")
                        .WithMany("Certificates")
                        .HasForeignKey("PondId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pond");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Collect.Collect", b =>
                {
                    b.HasOne("ShrimpPond.Domain.PondData.Collect.Pond", "Pond")
                        .WithMany()
                        .HasForeignKey("PondId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pond");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Collect.Pond", b =>
                {
                    b.HasOne("ShrimpPond.Domain.PondData.PondType", "PondType")
                        .WithMany()
                        .HasForeignKey("PondTypeId");

                    b.Navigation("PondType");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Feeding.Food.FoodForFeeding", b =>
                {
                    b.HasOne("ShrimpPond.Domain.PondData.Feeding.Food.FoodFeeding", null)
                        .WithMany("Foods")
                        .HasForeignKey("FoodFeedingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Feeding.Medicine.MedicineForFeeding", b =>
                {
                    b.HasOne("ShrimpPond.Domain.PondData.Feeding.Medicine.MedicineFeeding", null)
                        .WithMany("Medicines")
                        .HasForeignKey("MedicineFeedingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Collect.Collect", b =>
                {
                    b.Navigation("Certificates");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Collect.Pond", b =>
                {
                    b.Navigation("Certificates");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Feeding.Food.FoodFeeding", b =>
                {
                    b.Navigation("Foods");
                });

            modelBuilder.Entity("ShrimpPond.Domain.PondData.Feeding.Medicine.MedicineFeeding", b =>
                {
                    b.Navigation("Medicines");
                });
#pragma warning restore 612, 618
        }
    }
}
