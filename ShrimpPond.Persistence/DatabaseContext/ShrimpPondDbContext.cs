using Microsoft.EntityFrameworkCore;
using ShrimpPond.Domain.PondData;


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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShrimpPondDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
            // Xác định các thuộc tính làm key    
            modelBuilder.Entity<Pond>().HasKey(n => n.PondId);

            modelBuilder.Entity<PondType>().HasKey(n => n.PondTypeId);
  

            modelBuilder.Entity<Food>().HasKey(n => n.FoodId);
            modelBuilder.Entity<Food>().HasOne(p=>p.Pond).WithMany(p=>p.Foods).HasForeignKey(p=>p.PondId); 
            modelBuilder.Entity<Food>().Property(x => x.FoodId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Medicine>().HasKey(n => n.MedicineId);
            modelBuilder.Entity<Medicine>().HasOne(p => p.Pond).WithMany(p => p.Medicines).HasForeignKey(p => p.PondId);
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
