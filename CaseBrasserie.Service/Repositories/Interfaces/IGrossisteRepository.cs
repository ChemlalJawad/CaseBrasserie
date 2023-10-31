using CaseBrasserie.Application.Repositories.Commands.Grossistes;
using CaseBrasserie.Core.Entities;

namespace CaseBrasserie.Application.Repositories
{
    public interface IGrossisteRepository
    {
        // Task<IEnumerable<GrossisteBiere>> GetAll();
        GrossisteBiere AddNewBiereToGrosssite(AddNewBiereCommand command);
        GrossisteBiere UpdateGrossisteBiere(UpdateStockCommand command);
        decimal GetQuotation(QuotationCommand command);
    }
}
