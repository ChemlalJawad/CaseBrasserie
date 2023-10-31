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

        public Biere Add(CreateBiereCommand command)
        {

            if (command == null) throw new CommandeVideException();
            if (command.Nom == null) throw new BiereNomVideException();
            if (command.Prix <= 0) throw new BierePrixException();

            var brasserie = _context.Brasseries.SingleOrDefault(b => b.Id == command.BrasserieId);
            if (brasserie == null) { throw new BrasserieInexistantException(); }

            var addBiere = new Biere
            {
                Nom = command.Nom,
                DegreAlcool = command.DegreAlcool,
                Prix = command.Prix,
                Brasserie = brasserie
            };

            _context.Bieres.Add(addBiere);
            _context.SaveChanges();

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

        public IEnumerable<Biere> GetAll()
        {
            return _context.Bieres
                .Include(e => e.Brasserie)
                .ThenInclude(e => e.Bieres)
                .ThenInclude(e => e.GrossistesBieres)
                .ToList();
        }

        public Biere Delete(int biereId)
        {
            var biereDelete = _context.Bieres.SingleOrDefault(b => b.Id == biereId);
            if (biereDelete == null)
            {
                throw new BiereInexistantException();
            }
            _context.Bieres.Remove(biereDelete);
            _context.SaveChanges();

            return biereDelete;

        }
    }
}
