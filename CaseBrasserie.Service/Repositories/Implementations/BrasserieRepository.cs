using CaseBrasserie.Core.Entities;
using CaseBrasserie.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using static CaseBrasserie.Application.Exceptions.BrasserieCustomError;

namespace CaseBrasserie.Application.Repositories.Implementations
{
    public class BrasserieRepository : IBrasserieRepository
    {

        private readonly BrasserieContext _context;
        public BrasserieRepository(BrasserieContext context)
        {
            _context = context;
        }

        public async Task<Brasserie> FindBrasserieById(int id)
        {
            var brasserie = await _context.Brasseries
                .Include(b => b.Bieres)
                .ThenInclude(b => b.GrossistesBieres)
                .SingleOrDefaultAsync(b => b.Id == id);
            if (brasserie == null)
            {
                throw new BrasserieInexistantException();
            }

            return brasserie;
        }

        public async Task<IEnumerable<Brasserie>> GetAllBieres()
        {
            var brasseries = await _context.Brasseries
                .Include(b => b.Bieres)
                .ThenInclude(biere => biere.GrossistesBieres)
                .ThenInclude(grossisteBiere => grossisteBiere.Grossiste)
                .ToListAsync();

            return brasseries;
        }
    }
}
