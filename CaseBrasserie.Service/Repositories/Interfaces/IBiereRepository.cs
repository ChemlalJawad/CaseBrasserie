using CaseBrasserie.Application.Repositories.Commands.Bieres;
using CaseBrasserie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBrasserie.Application.Repositories.Interfaces
{
    public interface IBiereRepository
    {
        Task<IEnumerable<Biere>> GetAll();
        Task<Biere> GetById(int id);
        Task<Biere> Add(CreateBiereCommand biere);
        Task Delete(int id);
    }
}
