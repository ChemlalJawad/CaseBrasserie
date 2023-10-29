using CaseBrasserie.Application.Repositories;
using CaseBrasserie.Core.Entities;
using Microsoft.AspNetCore.Http;
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
        public  async Task<ActionResult<IEnumerable<Brasserie>>> GetAll() {
            var result = await _brasserieRepository.GetAllBieres();

            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Brasserie>> Get([FromRoute] int Id) {
            
            var result = await _brasserieRepository.FindBrasserieById(Id);

            return Ok(result);
        }
    }
}
