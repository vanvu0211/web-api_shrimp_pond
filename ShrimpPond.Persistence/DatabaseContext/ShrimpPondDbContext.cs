﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShrimpPond.Application.Feature.TimeSetting.Command.CreateTimeSetting;
using ShrimpPond.Domain.Environments;
using ShrimpPond.Domain.Farm;
using ShrimpPond.Domain.PondData;
using ShrimpPond.Domain.PondData.Feeding.Food;
using ShrimpPond.Domain.PondData.Feeding.Medicine;
using ShrimpPond.Domain.PondData.Harvest;
using ShrimpPond.Domain.TimeSetting;
using ShrimpPond.Domain.PondData.CleanSensor;


namespace ShrimpPond.Persistence.DatabaseContext
{
    public class ShrimpPondDbContext : DbContext
    {

        public ShrimpPondDbContext(DbContextOptions<ShrimpPondDbContext> options) : base(options)
        {

        }

        public DbSet<Pond> Pond { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<Medicine> Medicine { get; set; }
        public DbSet<Certificate> Certificate { get; set; }
        public DbSet<PondType> PondType { get; set; }
        public DbSet<CleanSensor> CleanSensor { get; set; }

        public DbSet<FoodFeeding> FoodFeeding { get; set; }
        public DbSet<FoodForFeeding> FoodForFeeding { get; set; }

        public DbSet<MedicineFeeding> MedicineFeeding { get; set; }
        public DbSet<MedicineForFeeding> MedicineForFeeding { get; set; }
        public DbSet<SizeShrimp> SizeShrimp { get; set; }
        public DbSet<LossShrimp> LossShrimp { get; set; }
        public DbSet<EnvironmentStatus> EnvironmentStatus { get; set; }
        public DbSet<Harvest> Harvests { get; set; }
        public DbSet<Farm> Farms { get; set; }
        public DbSet<TimeSetting> TimeSettings { get; set; }
        public DbSet<TimeSettingObject> timeSettingObjects { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShrimpPondDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
            // Xác định các thuộc tính làm key    
            modelBuilder.Entity<Pond>().HasKey(n => n.PondId);
            modelBuilder.Entity<PondType>().HasKey(n => n.PondTypeId);


            //Cho ăn
            modelBuilder.Entity<FoodForFeeding>().HasKey(n => n.FoodForFeedingId);
            modelBuilder.Entity<FoodForFeeding>().Property(x => x.FoodForFeedingId).ValueGeneratedOnAdd();
            modelBuilder.Entity<FoodFeeding>().HasKey(n => n.FoodFeedingId);
            modelBuilder.Entity<FoodFeeding>().Property(x => x.FoodFeedingId).ValueGeneratedOnAdd();

            //Điều trị
            modelBuilder.Entity<MedicineFeeding>().HasKey(n => n.MedicineFeedingId);
            modelBuilder.Entity<MedicineFeeding>().Property(x => x.MedicineFeedingId).ValueGeneratedOnAdd();
            modelBuilder.Entity<MedicineForFeeding>().HasKey(n => n.MedicineForFeedingId);
            modelBuilder.Entity<MedicineForFeeding>().Property(x => x.MedicineForFeedingId).ValueGeneratedOnAdd();


            modelBuilder.Entity<Food>().HasKey(n => n.FoodId);
            modelBuilder.Entity<Food>().Property(x => x.FoodId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Medicine>().HasKey(n => n.MedicineId);
            modelBuilder.Entity<Medicine>().Property(x => x.MedicineId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Certificate>().HasKey(n => n.CertificateId);
            modelBuilder.Entity<Certificate>().HasOne(p => p.Pond).WithMany(p => p.Certificates).HasForeignKey(p => p.PondId);
            modelBuilder.Entity<Certificate>().Property(x => x.CertificateId).ValueGeneratedOnAdd();

            modelBuilder.Entity<LossShrimp>().HasKey(n => n.LossShrimpId);
            modelBuilder.Entity<LossShrimp>().Property(x => x.LossShrimpId).ValueGeneratedOnAdd();

            modelBuilder.Entity<SizeShrimp>().HasKey(n => n.SizeShrimpId);
            modelBuilder.Entity<SizeShrimp>().Property(x => x.SizeShrimpId).ValueGeneratedOnAdd();

            modelBuilder.Entity<EnvironmentStatus>().HasKey(n => n.EnvironmentStatusId);
            modelBuilder.Entity<EnvironmentStatus>().Property(x => x.EnvironmentStatusId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Harvest>().HasKey(n => n.HarvestId);
            modelBuilder.Entity<Harvest>().Property(x => x.HarvestId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Farm>().HasKey(n => n.FarmId);
            modelBuilder.Entity<Farm>().Property(x => x.FarmId).ValueGeneratedOnAdd();

            modelBuilder.Entity<TimeSetting>().HasKey(n => n.TimeSettingId);
            modelBuilder.Entity<TimeSetting>().Property(x => x.TimeSettingId).ValueGeneratedOnAdd();

            modelBuilder.Entity<TimeSettingObject>().HasKey(n => n.TimeSettingObjectId);
            modelBuilder.Entity<TimeSettingObject>().Property(x => x.TimeSettingObjectId).ValueGeneratedOnAdd(); 
            
            modelBuilder.Entity<CleanSensor>().HasKey(n => n.CleanSensorId);
            modelBuilder.Entity<CleanSensor>().Property(x => x.CleanSensorId).ValueGeneratedOnAdd();

        }

    }
}
