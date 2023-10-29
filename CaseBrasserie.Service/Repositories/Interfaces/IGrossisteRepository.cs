using CaseBrasserie.Application.Repositories.Commands.Grossistes;
using CaseBrasserie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBrasserie.Application.Repositories
{
    public interface IGrossisteRepository
    {
        Task<IEnumerable<GrossisteBiere>> GetAll();
        Task AddNewBiereToGrosssite(AddNewBiereCommand command);
        Task UpdateGrossisteBiere(UpdateStockCommand command);
        Task<decimal> GetQuotation(QuotationCommand command);
    }
}
