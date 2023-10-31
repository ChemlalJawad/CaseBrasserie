using CaseBrasserie.Application.Repositories.Commands.Grossistes;
using CaseBrasserie.Core.Entities;
using CaseBrasserie.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using static CaseBrasserie.Application.Exceptions.BrasserieCustomError;

namespace CaseBrasserie.Application.Repositories.Implementations
{
    public class GrossisteRepository : IGrossisteRepository
    {
        private readonly IBrasserieContext _context;
        public GrossisteRepository(IBrasserieContext context)
        {
            _context = context;
        }

        public GrossisteBiere AddNewBiereToGrosssite(AddNewBiereCommand command)
        {
            if (command == null) throw new CommandeVideException();
            if (command.Stock < 0) throw new StockInsuffisantException();

            var biere = _context.Bieres.FirstOrDefault(b => b.Id == command.BiereId);
            if (biere == null)
            {
                throw new BiereInexistantException();
            }
            var grossiste = _context.Grossistes
                .Include(e => e.GrossistesBieres)
                .FirstOrDefault(b => b.Id == command.GrossisteId);
            if (grossiste == null)
            {
                throw new GrossisteInexistantException();
            }
            if (grossiste.GrossistesBieres.Any(gb => gb.GrossisteId == command.GrossisteId && gb.BiereId == command.BiereId))
            {
                throw new BiereDejaVendueParGrossisteException();
            }

            var addBiereToGrossiste = new GrossisteBiere()
            {
                GrossisteId = command.GrossisteId,
                BiereId = command.BiereId,
                Stock = command.Stock
            };

            _context.GrossistesBieres.Add(addBiereToGrossiste);
            _context.SaveChanges();

            return addBiereToGrossiste;
        }

        public decimal GetQuotation(QuotationCommand command)
        {
            if (command.Items == null || command.Items?.Count() < 1 || command == null)
                throw new CommandeVideException();

            var grossiste = _context.Grossistes
                                    .Include(e => e.GrossistesBieres)
                                    .ThenInclude(e => e.Biere)
                                    .FirstOrDefault(e => e.Id == command.GrossisteId);
            if (grossiste == null)
                throw new GrossisteInexistantException();



            if (command.Items?.GroupBy(e => e.BiereId).ToList().Count() < command.Items?.Count())
            {
                throw new DoublonCommandeException();
            }

            var prixTotal = 0.00M;


            foreach (var item in command.Items)
            {
                var gb = grossiste.GrossistesBieres.FirstOrDefault(gb => gb.BiereId == item.BiereId);

                if (gb == null)
                {
                    throw new BiereNonVendueParGrossisteException();
                }
                if (gb.Stock >= item.Quantite)
                {
                    prixTotal += gb.Biere.Prix * item.Quantite;
                }
                else
                {
                    throw new StockInsuffisantException();
                }
            }

            var stockTotal = 0;

            foreach (var item in command.Items)
            {
                stockTotal += item.Quantite;
            }
            if (stockTotal > 20)
            {
                prixTotal *= 0.8M;
            }
            else if (stockTotal > 10)
            {
                prixTotal *= 0.9M;
            }

            return prixTotal;
        }

        public GrossisteBiere UpdateGrossisteBiere(UpdateStockCommand command)
        {
            if (command == null) { throw new CommandeVideException(); }

            var grossisteBiere = _context.GrossistesBieres.SingleOrDefault(gb => gb.GrossisteId == command.GrossisteId && gb.BiereId == command.BiereId);
            if (grossisteBiere == null) { throw new GrossisteBiereException(); }
            if (command.Stock < 0) { throw new StockModificationException(); }

            grossisteBiere.Stock = command.Stock;

            _context.Update(grossisteBiere);
            _context.SaveChanges();

            return grossisteBiere;
        }
    }
}
