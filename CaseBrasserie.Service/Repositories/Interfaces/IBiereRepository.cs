using CaseBrasserie.Application.Repositories.Commands.Bieres;
using CaseBrasserie.Core.Entities;

namespace CaseBrasserie.Application.Repositories.Interfaces
{
    public interface IBiereRepository
    {
        IEnumerable<Biere> GetAll();
        Task<Biere> GetById(int id);
        Biere Add(CreateBiereCommand biere);
        Biere Delete(int id);
    }
}
