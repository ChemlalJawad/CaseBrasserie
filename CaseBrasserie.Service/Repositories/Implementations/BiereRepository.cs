using CaseBrasserie.Application.Repositories.Commands.Bieres;
using CaseBrasserie.Application.Repositories.Interfaces;
using CaseBrasserie.Core.Entities;
using CaseBrasserie.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using static CaseBrasserie.Application.Exceptions.BrasserieCustomError;

namespace CaseBrasserie.Application.Repositories.Implementations
{
    public class BiereRepository : IBiereRepository
    {

        private readonly IBrasserieContext _context;

        public BiereRepository(IBrasserieContext context)
        {
            _context = context;
        }

        public async Task<Biere> Add(CreateBiereCommand command)
        {

            if (command == null) throw new CommandeVideException();
            if (command.Nom == null) throw new BiereNomVideException();
            if (command.Prix <= 0) throw new BierePrixException();

            var brasserie = await _context.Brasseries.SingleOrDefaultAsync(b => b.Id == command.BrasserieId);
            if (brasserie == null) { throw new BrasserieInexistantException(); }

            var addBiere = new Biere
            {
                Nom = command.Nom,
                DegreAlcool = command.DegreAlcool,
                Prix = command.Prix,
                Brasserie = brasserie
            };

            await _context.Bieres.AddAsync(addBiere);
            await _context.SaveChangesAsync();

            return addBiere;
        }

        public async Task<Biere> GetById(int id)
        {
            var biere = await _context.Bieres.FirstOrDefaultAsync(b => b.Id == id);

            if (biere == null)
            {
                throw new BiereInexistantException();
            }
            return biere;
        }

        public async Task<IEnumerable<Biere>> GetAll()
        {
            return await _context.Bieres
                .Include(e => e.Brasserie)
                .ThenInclude(e => e.Bieres)
                .ThenInclude(e => e.GrossistesBieres)
                .ToListAsync();
        }

        public async Task Delete(int biereId)
        {
            var biereDelete = await _context.Bieres.SingleOrDefaultAsync(b => b.Id == biereId);
            if (biereDelete == null)
            {
                throw new BiereInexistantException();
            }
            _context.Bieres.Remove(new Biere() { Id = biereId });
            await _context.SaveChangesAsync();

        }
    }
}
