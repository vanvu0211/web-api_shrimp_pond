﻿using Microsoft.EntityFrameworkCore;
using ShrimpPond.Domain.PondData;
using ShrimpPond.Domain.PondData.Collect;
using ShrimpPond.Domain.PondData.Feeding.Food;
using ShrimpPond.Domain.PondData.Feeding.Medicine;


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
        public DbSet<Certificate> Certificate {  get; set; } 
        public DbSet<PondType> PondType {  get; set; }

        public DbSet<FoodFeeding> FoodFeeding {  get; set; }
        public DbSet<FoodForFeeding> FoodForFeeding {  get; set; }

        public DbSet<MedicineFeeding> MedicineFeeding { get; set; }
        public DbSet<MedicineForFeeding> MedicineForFeeding { get; set; }

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
            modelBuilder.Entity<LossShrimp>().HasOne(p => p.Pond).WithOne(p => p.LossShrimp);
            modelBuilder.Entity<LossShrimp>().Property(x => x.LossShrimpId).ValueGeneratedOnAdd();

            modelBuilder.Entity<SizeShrimp>().HasKey(n => n.SizeShrimpId);
            //modelBuilder.Entity<SizeShrimp>().HasOne(p => p.Pond).WithOne(p => p.sizeShrimp);
            modelBuilder.Entity<SizeShrimp>().Property(x => x.SizeShrimpId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Collect>().HasKey(n => n.CollectId);
            modelBuilder.Entity<Collect>().HasOne(p => p.Pond).WithOne(p => p.Collect);
            modelBuilder.Entity<Collect>().Property(x => x.CollectId).ValueGeneratedOnAdd();
        }
    }
}