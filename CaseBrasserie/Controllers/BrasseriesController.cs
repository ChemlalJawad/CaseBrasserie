using CaseBrasserie.Application.Repositories;
using CaseBrasserie.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CaseBrasserie.Web.Controllers
{
    [Route("api/brasseries")]
    [ApiController]
    public class BrasseriesController : ControllerBase
    {
        private readonly IBrasserieRepository _brasserieRepository;
        public BrasseriesController(IBrasserieRepository brasserieRepository)
        {
            _brasserieRepository = brasserieRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Brasserie>> GetAll()
        {
            var result = _brasserieRepository.GetAllBrasseries();

            return Ok(result);
        }

        [HttpGet("{Id}")]
        public ActionResult<Brasserie> Get([FromRoute] int Id)
        {

            var result = _brasserieRepository.FindBrasserieById(Id);

            return Ok(result);
        }
    }
}
