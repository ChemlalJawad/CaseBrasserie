
using CaseBrasserie.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseBrasserie.Infrastructure.Data.DbContexts
{
    public class BrasserieContext : DbContext, IBrasserieContext
    {
        public BrasserieContext(DbContextOptions<BrasserieContext> options) : base(options) { }
        public DbSet<Brasserie> Brasseries { get; set; }
        public DbSet<Biere> Bieres { get; set; }
        public DbSet<Grossiste> Grossistes { get; set; }
        public DbSet<GrossisteBiere> GrossistesBieres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configuration pour la relation Many-to-Many entre Biere et Grossiste
            modelBuilder.Entity<GrossisteBiere>()
                .HasKey(gb => new { gb.BiereId, gb.GrossisteId });  // Clé composée

            modelBuilder.Entity<GrossisteBiere>()
                .HasOne(gb => gb.Biere)
                .WithMany(b => b.GrossistesBieres)
                .HasForeignKey(gb => gb.BiereId);

            modelBuilder.Entity<GrossisteBiere>()
                .HasOne(gb => gb.Grossiste)
                .WithMany(g => g.GrossistesBieres)
                .HasForeignKey(gb => gb.GrossisteId);

            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            Set<TEntity>().Update(entity);
        }
    }

}
