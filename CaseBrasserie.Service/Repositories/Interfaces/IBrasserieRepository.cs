using CaseBrasserie.Core.Entities;

namespace CaseBrasserie.Application.Repositories
{
    public interface IBrasserieRepository
    {
        IEnumerable<Brasserie> GetAllBrasseries();
        Brasserie FindBrasserieById(int id);
    }
}
