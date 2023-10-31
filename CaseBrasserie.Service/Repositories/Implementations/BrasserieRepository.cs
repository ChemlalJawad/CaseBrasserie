using CaseBrasserie.Core.Entities;
using CaseBrasserie.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using static CaseBrasserie.Application.Exceptions.BrasserieCustomError;

namespace CaseBrasserie.Application.Repositories.Implementations
{
    public class BrasserieRepository : IBrasserieRepository
    {

        private readonly IBrasserieContext _context;
        public BrasserieRepository(IBrasserieContext context)
        {
            _context = context;
        }

        public Brasserie FindBrasserieById(int id)
        {
            var brasserie = _context.Brasseries
                .Include(b => b.Bieres)
                .ThenInclude(b => b.GrossistesBieres)
                .SingleOrDefault(b => b.Id == id);
            if (brasserie == null)
            {
                throw new BrasserieInexistantException();
            }

            return brasserie;
        }

        public IEnumerable<Brasserie> GetAllBrasseries()
        {
            var brasseries = _context.Brasseries
                .Include(b => b.Bieres)
                .ThenInclude(biere => biere.GrossistesBieres)
                .ThenInclude(grossisteBiere => grossisteBiere.Grossiste)
                .ToList();

            return brasseries;
        }
    }
}
