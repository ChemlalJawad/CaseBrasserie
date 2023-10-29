using CaseBrasserie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBrasserie.Application.Repositories
{
    public interface IBrasserieRepository
    {
        Task<IEnumerable<Brasserie>> GetAllBieres();
        Task<Brasserie> FindBrasserieById(int id);
    }
}
