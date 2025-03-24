﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShrimpPond.Application.Feature.TimeSetting.Command.CreateTimeSetting;
using ShrimpPond.Domain.Environments;
using ShrimpPond.Domain.Farm;
using ShrimpPond.Domain.PondData;
using ShrimpPond.Domain.PondData.Harvest;
using ShrimpPond.Domain.TimeSetting;
using ShrimpPond.Domain.PondData.CleanSensor;
using ShrimpPond.Domain.Food;
using ShrimpPond.Domain.Medicine;


namespace ShrimpPond.Persistence.DatabaseContext
{
    public class ShrimpPondDbContext : DbContext
    {

        public ShrimpPondDbContext(DbContextOptions<ShrimpPondDbContext> options) : base(options)
        {
            Database.SetCommandTimeout(60); 
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


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShrimpPondDbContext).Assembly);
        //    base.OnModelCreating(modelBuilder);
        //    // Xác định các thuộc tính làm key    
        //    modelBuilder.Entity<Pond>().HasKey(n => n.pondId);
        //    modelBuilder.Entity<PondType>().HasKey(n => n.pondTypeId);


        //    //Cho ăn
        //    modelBuilder.Entity<FoodForFeeding>().HasKey(n => n.foodForFeedingId);
        //    modelBuilder.Entity<FoodForFeeding>().Property(x => x.foodForFeedingId).ValueGeneratedOnAdd();
        //    modelBuilder.Entity<FoodFeeding>().HasKey(n => n.foodFeedingId);
        //    modelBuilder.Entity<FoodFeeding>().Property(x => x.foodFeedingId).ValueGeneratedOnAdd();

        //    //Điều trị
        //    modelBuilder.Entity<MedicineFeeding>().HasKey(n => n.medicineFeedingId);
        //    modelBuilder.Entity<MedicineFeeding>().Property(x => x.medicineFeedingId).ValueGeneratedOnAdd();
        //    modelBuilder.Entity<MedicineForFeeding>().HasKey(n => n.medicineForFeedingId);
        //    modelBuilder.Entity<MedicineForFeeding>().Property(x => x.medicineForFeedingId).ValueGeneratedOnAdd();


        //    modelBuilder.Entity<Food>().HasKey(n => n.foodId);
        //    modelBuilder.Entity<Food>().Property(x => x.foodId).ValueGeneratedOnAdd();

        //    modelBuilder.Entity<Medicine>().HasKey(n => n.medicineId);
        //    modelBuilder.Entity<Medicine>().Property(x => x.medicineId).ValueGeneratedOnAdd();

        //    modelBuilder.Entity<Certificate>().HasKey(n => n.certificateId);
        //    modelBuilder.Entity<Certificate>().HasOne(p => p.pond).WithMany(p => p.certificates).HasForeignKey(p => p.pondId);
        //    modelBuilder.Entity<Certificate>().Property(x => x.certificateId).ValueGeneratedOnAdd();

        //    modelBuilder.Entity<LossShrimp>().HasKey(n => n.lossShrimpId);
        //    modelBuilder.Entity<LossShrimp>().Property(x => x.lossShrimpId).ValueGeneratedOnAdd();

        //    modelBuilder.Entity<SizeShrimp>().HasKey(n => n.sizeShrimpId);
        //    modelBuilder.Entity<SizeShrimp>().Property(x => x.sizeShrimpId).ValueGeneratedOnAdd();

        //    modelBuilder.Entity<EnvironmentStatus>().HasKey(n => n.environmentStatusId);
        //    modelBuilder.Entity<EnvironmentStatus>().Property(x => x.environmentStatusId).ValueGeneratedOnAdd();

        //    modelBuilder.Entity<Harvest>().HasKey(n => n.harvestId);
        //    modelBuilder.Entity<Harvest>().Property(x => x.harvestId).ValueGeneratedOnAdd();

        //    modelBuilder.Entity<Farm>().HasKey(n => n.farmId);
        //    modelBuilder.Entity<Farm>().Property(x => x.farmId).ValueGeneratedOnAdd();

        //    modelBuilder.Entity<TimeSetting>().HasKey(n => n.timeSettingId);
        //    modelBuilder.Entity<TimeSetting>().Property(x => x.timeSettingId).ValueGeneratedOnAdd();

        //    modelBuilder.Entity<TimeSettingObject>().HasKey(n => n.timeSettingObjectId);
        //    modelBuilder.Entity<TimeSettingObject>().Property(x => x.timeSettingObjectId).ValueGeneratedOnAdd(); 
            
        //    modelBuilder.Entity<CleanSensor>().HasKey(n => n.cleanSensorId);
        //    modelBuilder.Entity<CleanSensor>().Property(x => x.cleanSensorId).ValueGeneratedOnAdd();

        //}



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Farm -> CleanSensor (1-n)
            modelBuilder.Entity<CleanSensor>()
                .HasOne(cs => cs.farm)
                .WithMany()
                .HasForeignKey(cs => cs.farmId)
                .OnDelete(DeleteBehavior.NoAction); // Tắt cascade delete để tránh xung đột


            // Pond -> EnvironmentStatus (1-n)
            modelBuilder.Entity<EnvironmentStatus>()
                .HasOne(es => es.pond)
                .WithMany(p => p.environmentStatus)
                .HasForeignKey(es => es.pondId)
                .OnDelete(DeleteBehavior.NoAction); // Khi xóa Pond, xóa EnvironmentStatus

            // Farm -> Food (1-n)
            modelBuilder.Entity<Food>()
                .HasOne(f => f.farm)
                .WithMany()
                .HasForeignKey(f => f.farmId)
                .OnDelete(DeleteBehavior.NoAction);


            // Pond -> FoodFeeding (1-n)
            modelBuilder.Entity<FoodFeeding>()
                .HasOne(ff => ff.pond)
                .WithMany(p => p.foodFeedings)
                .HasForeignKey(ff => ff.pondId)
                .OnDelete(DeleteBehavior.NoAction);

            // FoodFeeding -> FoodForFeeding (1-n)
            modelBuilder.Entity<FoodForFeeding>()
                .HasOne(fff => fff.foodFeeding)
                .WithMany(ff => ff.foods)
                .HasForeignKey(fff => fff.foodFeedingId)
                .OnDelete(DeleteBehavior.NoAction);



            // Pond -> Harvest (1-n)
            modelBuilder.Entity<Harvest>()
                .HasOne(h => h.pond)
                .WithMany(p => p.harvests)
                .HasForeignKey(h => h.pondId)
                .OnDelete(DeleteBehavior.NoAction);

            // Farm -> Medicine (1-n)
            modelBuilder.Entity<Medicine>()
                .HasOne(m => m.farm)
                .WithMany()
                .HasForeignKey(m => m.farmId)
                .OnDelete(DeleteBehavior.NoAction);


            // Pond -> MedicineFeeding (1-n)
            modelBuilder.Entity<MedicineFeeding>()
                .HasOne(mf => mf.pond)
                .WithMany(p => p.medicineFeedings)
                .HasForeignKey(mf => mf.pondId)
                .OnDelete(DeleteBehavior.NoAction);

            // MedicineFeeding -> MedicineForFeeding (1-n)
            modelBuilder.Entity<MedicineForFeeding>()
                .HasOne(mff => mff.medicineFeeding)
                .WithMany(mf => mf.medicines)
                .HasForeignKey(mff => mff.medicineFeedingId)
                .OnDelete(DeleteBehavior.NoAction);



            // Pond -> Certificate (1-n)
            modelBuilder.Entity<Certificate>()
                .HasOne(c => c.pond)
                .WithMany(p => p.certificates)
                .HasForeignKey(c => c.pondId)
                .OnDelete(DeleteBehavior.NoAction);



            // Pond -> LossShrimp (1-n)
            modelBuilder.Entity<LossShrimp>()
                .HasOne(ls => ls.pond)
                .WithMany(p => p.lossShrimps)
                .HasForeignKey(ls => ls.pondId)
                .OnDelete(DeleteBehavior.NoAction);


            // PondType -> Pond (1-n)
            modelBuilder.Entity<Pond>()
       .HasOne(p => p.pondType)
       .WithMany()
       .HasForeignKey(p => p.pondTypeId)
       .IsRequired(false)
       .OnDelete(DeleteBehavior.NoAction); // Ngăn chặn tự động cập nhật/xóa

            // Farm -> PondType (1-n)
            modelBuilder.Entity<PondType>()
                .HasOne(pt => pt.farm)
                .WithMany()
                .HasForeignKey(pt => pt.farmId)
                .OnDelete(DeleteBehavior.NoAction);



            // Pond -> SizeShrimp (1-n)
            modelBuilder.Entity<SizeShrimp>()
                .HasOne(ss => ss.pond)
                .WithMany(p => p.sizeShrimps)
                .HasForeignKey(ss => ss.pondId)
                .OnDelete(DeleteBehavior.NoAction);

            // Farm -> TimeSetting (1-n)
            modelBuilder.Entity<TimeSetting>()
                .HasOne(ts => ts.farm)
                .WithMany()
                .HasForeignKey(ts => ts.farmId)
                .OnDelete(DeleteBehavior.NoAction);

            // TimeSetting -> TimeSettingObject (1-n)
            modelBuilder.Entity<TimeSettingObject>()
                .HasOne(tso => tso.timeSetting)
                .WithMany(ts => ts.timeSettingObjects)
                .HasForeignKey(tso => tso.timeSettingId)
                .OnDelete(DeleteBehavior.Cascade);

            // Đặt khóa chính cho các entity
            modelBuilder.Entity<CleanSensor>().HasKey(cs => cs.cleanSensorId);
            modelBuilder.Entity<CleanSensor>().Property(cs => cs.cleanSensorId).ValueGeneratedOnAdd();
            modelBuilder.Entity<EnvironmentStatus>().HasKey(es => es.environmentStatusId);
            modelBuilder.Entity<EnvironmentStatus>().Property(cs => cs.environmentStatusId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Farm>().HasKey(f => f.farmId);
            modelBuilder.Entity<Farm>().Property(cs => cs.farmId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Food>().HasKey(f => f.foodId);
            modelBuilder.Entity<Food>().Property(cs => cs.foodId).ValueGeneratedOnAdd();
            modelBuilder.Entity<FoodFeeding>().HasKey(ff => ff.foodFeedingId);
            modelBuilder.Entity<FoodFeeding>().Property(cs => cs.foodFeedingId).ValueGeneratedOnAdd();
            modelBuilder.Entity<FoodForFeeding>().HasKey(fff => fff.foodForFeedingId);
            modelBuilder.Entity<FoodForFeeding>().Property(cs => cs.foodForFeedingId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Harvest>().HasKey(h => h.harvestId);
            modelBuilder.Entity<Harvest>().Property(cs => cs.harvestId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Medicine>().HasKey(m => m.medicineId);
            modelBuilder.Entity<Medicine>().Property(cs => cs.medicineId).ValueGeneratedOnAdd();
            modelBuilder.Entity<MedicineFeeding>().HasKey(mf => mf.medicineFeedingId);
            modelBuilder.Entity<MedicineFeeding>().Property(cs => cs.medicineFeedingId).ValueGeneratedOnAdd();
            modelBuilder.Entity<MedicineForFeeding>().HasKey(mff => mff.medicineForFeedingId);
            modelBuilder.Entity<MedicineForFeeding>().Property(cs => cs.medicineForFeedingId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Certificate>().HasKey(c => c.certificateId);
            modelBuilder.Entity<Certificate>().Property(cs => cs.certificateId).ValueGeneratedOnAdd();
            modelBuilder.Entity<LossShrimp>().HasKey(ls => ls.lossShrimpId);
            modelBuilder.Entity<LossShrimp>().Property(cs => cs.lossShrimpId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Pond>().HasKey(p => p.pondId);
            modelBuilder.Entity<PondType>().HasKey(pt => pt.pondTypeId);
            modelBuilder.Entity<SizeShrimp>().HasKey(ss => ss.sizeShrimpId);
            modelBuilder.Entity<SizeShrimp>().Property(cs => cs.sizeShrimpId).ValueGeneratedOnAdd();
            modelBuilder.Entity<TimeSetting>().HasKey(ts => ts.timeSettingId);
            modelBuilder.Entity<TimeSetting>().Property(cs => cs.timeSettingId).ValueGeneratedOnAdd();
            modelBuilder.Entity<TimeSettingObject>().HasKey(tso => tso.timeSettingObjectId);
            modelBuilder.Entity<TimeSettingObject>().Property(cs => cs.timeSettingObjectId).ValueGeneratedOnAdd();
        }
    }
}
