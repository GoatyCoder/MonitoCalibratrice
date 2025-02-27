using Microsoft.EntityFrameworkCore;
using MonitoCalibratrice.Domain.Entities;
using MonitoCalibratrice.Domain.Entities.Interfaces;

namespace MonitoCalibratrice.Infrastructure
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<RawProduct> RawProducts => Set<RawProduct>();
        public DbSet<Variety> Varieties => Set<Variety>();
        public DbSet<SecondaryPackaging> SecondaryPackagings => Set<SecondaryPackaging>();
        public DbSet<FinishedProduct> FinishedProducts => Set<FinishedProduct>();
        public DbSet<ProductionBatch> ProductionBatches => Set<ProductionBatch>();
        public DbSet<FinishedProductPallet> FinishedProductPallets => Set<FinishedProductPallet>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                .Where(t => typeof(IHasCode).IsAssignableFrom(t.ClrType)))
            {
                if (entityType.ClrType == typeof(Variety))
                {
                    modelBuilder.Entity<Variety>()
                       .HasIndex(v => new { v.RawProductId, v.Code })
                       .IsUnique();
                }
                else
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .HasIndex(nameof(IHasCode.Code))
                        .IsUnique();
                }
            }

            modelBuilder.Entity<FinishedProductPallet>()
                .HasIndex(p => p.PalletCode)
                .IsUnique();
        }
    }
}
