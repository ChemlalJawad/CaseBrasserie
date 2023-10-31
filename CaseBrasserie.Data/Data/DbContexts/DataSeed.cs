using CaseBrasserie.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseBrasserie.Infrastructure.Data.DbContexts
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Seeding de données pour Brasserie
            modelBuilder.Entity<Brasserie>().HasData(new Brasserie
            {
                Id = 1,
                Nom = "Abbaye de Leffe"
            },
            new Brasserie
            {
                Id = 2,
                Nom = "Brasserie de Chimay"
            });

            // Seeding de données pour Biere
            modelBuilder.Entity<Biere>().HasData(new Biere
            {
                Id = 1,
                Nom = "Leffe Blonde",
                DegreAlcool = 6.6F,
                Prix = 2.20M,
                BrasserieId = 1  // relation avec la Brasserie "Abbaye de Leffe"
            },
            new Biere
            {
                Id = 2,
                Nom = "Chimay Bleue",
                DegreAlcool = 9.0F,
                Prix = 3.50M,
                BrasserieId = 2  // relation avec la "Brasserie de Chimay"
            },
            new Biere
            {
                Id = 3,
                Nom = "Chimay Rouge",
                DegreAlcool = 7.0F,
                Prix = 3.00M,
                BrasserieId = 2  // relation avec la "Brasserie de Chimay"
            });

            // Seeding de données pour Grossiste
            modelBuilder.Entity<Grossiste>().HasData(new Grossiste
            {
                Id = 1,
                Nom = "GeneDrinks"
            },
            new Grossiste
            {
                Id = 2,
                Nom = "BiereImport"
            });

            modelBuilder.Entity<GrossisteBiere>().HasData(
                new GrossisteBiere
                {
                    GrossisteId = 1,  // GeneDrinks
                    BiereId = 1,      // Leffe Blonde
                    Stock = 100       // Stock de 100 unités
                },
                new GrossisteBiere
                {
                    GrossisteId = 1,  // GeneDrinks
                    BiereId = 2,      // Chimay Bleue
                    Stock = 80        // Stock de 80 unités
                },
                new GrossisteBiere
                {
                    GrossisteId = 2,  // BiereImport
                    BiereId = 2,      // Chimay Bleue
                    Stock = 120       // Stock de 120 unités
                },
                new GrossisteBiere
                {
                    GrossisteId = 2,  // BiereImport
                    BiereId = 3,      // Chimay Rouge
                    Stock = 90        // Stock de 90 unités
                }
            );
        }
    }
}
