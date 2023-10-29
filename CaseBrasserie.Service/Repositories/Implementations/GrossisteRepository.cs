using CaseBrasserie.Application.Repositories.Commands.Grossistes;
using CaseBrasserie.Core.Entities;
using CaseBrasserie.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using static CaseBrasserie.Application.Exceptions.BrasserieCustomError;

namespace CaseBrasserie.Application.Repositories.Implementations
{
    public class GrossisteRepository : IGrossisteRepository
    {
        private readonly BrasserieContext _context;
        public GrossisteRepository(BrasserieContext context)
        {
            _context = context;
        }

        public async Task AddNewBiereToGrosssite(AddNewBiereCommand command)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var biere = await _context.Bieres.FirstOrDefaultAsync(b => b.Id == command.BiereId);
                if (biere == null)
                {
                    throw new BiereInexistantException();
                }
                var grossiste = await _context.Grossistes
                    .Include(e => e.GrossistesBieres)
                    .FirstOrDefaultAsync(b => b.Id == command.GrossisteId);
                if (grossiste == null)
                {
                    throw new GrossisteInexistantException();
                }
                if (grossiste.GrossistesBieres.Any(gb => gb.GrossisteId == command.GrossisteId && gb.BiereId == command.BiereId))
                {
                    throw new BiereDejaVendueParGrossisteException();
                }

                var addBiere = new GrossisteBiere()
                {
                    GrossisteId = command.GrossisteId,
                    BiereId = command.BiereId,
                    Stock = command.Stock
                };

                await _context.GrossistesBieres.AddAsync(addBiere);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new TransactionAjoutBiereDansGrossisteError();
            }

        }

        public async Task<decimal> GetQuotation(QuotationCommand command)
        {
            if (command.Items == null)
                throw new CommandeVideException();

            var grossiste = await _context.Grossistes
                                    .Include(e => e.GrossistesBieres)
                                    .ThenInclude(e => e.Biere)
                                    .SingleOrDefaultAsync(e => e.Id == command.GrossisteId);
            if (grossiste == null)
                throw new GrossisteInexistantException();


            if (command.Items.GroupBy(e => e.BiereId).ToList().Count() < command.Items.Count()) {
                throw new DoublonCommandeException();
            }

            var prixTotal = 0.00M;


            foreach (var item in command.Items)
            {
                var gb = grossiste.GrossistesBieres.SingleOrDefault(gb => gb.BiereId == item.BiereId);

                if(gb == null)
                {
                    throw new BiereNonVendueParGrossisteException();
                }
                if(gb.Stock >= item.Quantite)
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
            if(stockTotal > 20 )
            {
                prixTotal *= 0.8M;  
            }
            else if ( stockTotal > 10)
            {
                prixTotal *= 0.9M;
            }

            return prixTotal;
        }

        public async Task<IEnumerable<GrossisteBiere>> GetAll()
        {
            return await _context.GrossistesBieres
                .Include(e => e.Grossiste)
                .ThenInclude(e => e.GrossistesBieres)
                .ThenInclude(e => e.Biere)
                .ToListAsync();
        }

        public async Task UpdateGrossisteBiere(UpdateStockCommand command)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (command == null) { throw new CommandeVideException(); }

                var grossisteBiere = await _context.GrossistesBieres.SingleOrDefaultAsync(gb => gb.GrossisteId == command.BiereId && gb.BiereId == command.GrossisteId);
                if( grossisteBiere == null ) { throw new GrossisteBiereException();}
                if (command.Stock < 0) { throw new StockModificationException(); }

                grossisteBiere.Stock = command.Stock;

                _context.Update(grossisteBiere);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            } catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new TransactionModificationStockBiereDansGrossisteError();
            }

        }
    }
}
