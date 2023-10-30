using CaseBrasserie.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBrasserie.Infrastructure.Data.DbContexts
{
    public  interface IBrasserieContext
    {
        public DbSet<Brasserie> Brasseries { get; set; }
        public DbSet<Biere> Bieres { get; set; }
        public DbSet<Grossiste> Grossistes { get; set; }
        public DbSet<GrossisteBiere> GrossistesBieres { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
