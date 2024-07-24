using Microsoft.EntityFrameworkCore;
using ShrimpPond.Domain;


namespace ShrimpPond.Persistence.DatabaseContext
{
    public class ShrimpPondDbContext : DbContext
    {

        public ShrimpPondDbContext(DbContextOptions<ShrimpPondDbContext> options) : base(options)
        {

        }

        public DbSet<NurseryPond> NurseryPond { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<Medicine> Medicine { get; set; }
        public DbSet<PHValue> PHValue { get; set; }
        public DbSet<TemperatureValue> TemperatureValue { get; set; }
        public DbSet<EnvironmentPara> EnvironmentPara { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShrimpPondDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
            // Xác định các thuộc tính làm key    
            modelBuilder.Entity<NurseryPond>().HasKey(n => n.PondId);

            modelBuilder.Entity<Food>().HasKey(n => n.FoodId);
            modelBuilder.Entity<Food>().Property(x => x.FoodId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Medicine>().HasKey(n => n.MedicineId);
            modelBuilder.Entity<Medicine>().Property(x => x.MedicineId).ValueGeneratedOnAdd();

            modelBuilder.Entity<PHValue>().HasKey(n => n.PhId);
            modelBuilder.Entity<PHValue>().Property(x => x.PhId).ValueGeneratedOnAdd();

            modelBuilder.Entity<TemperatureValue>().HasKey(n => n.TemperatureId);
            modelBuilder.Entity<TemperatureValue>().Property(x => x.TemperatureId).ValueGeneratedOnAdd();

            modelBuilder.Entity<EnvironmentPara>().HasKey(n => n.EnvironmentId);
            modelBuilder.Entity<EnvironmentPara>().Property(x => x.EnvironmentId).ValueGeneratedOnAdd();


        }
    }
}
