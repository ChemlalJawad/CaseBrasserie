using CaseBrasserie.Application.Repositories.Commands.Grossistes;
using CaseBrasserie.Core.Entities;

namespace CaseBrasserie.Application.Repositories
{
    public interface IGrossisteRepository
    {
        GrossisteBiere AddNewBiereToGrosssite(AddNewBiereCommand command);
        GrossisteBiere UpdateGrossisteBiere(UpdateStockCommand command);
        decimal GetQuotation(QuotationCommand command);
    }
}
