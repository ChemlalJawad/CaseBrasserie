using CaseBrasserie.Application.Repositories.Commands.Bieres;
using CaseBrasserie.Application.Repositories.Interfaces;
using CaseBrasserie.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CaseBrasserie.Web.Controllers
{
    [Route("api/bieres")]
    [ApiController]
    public class BieresController : ControllerBase
    {
        private readonly IBiereRepository _biereRepository;
        public BieresController(IBiereRepository biereRepository)
        {
            _biereRepository = biereRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Biere>>> GetAll()
        {
            var bieres = await _biereRepository.GetAll();

            return Ok(bieres);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Biere>> GetBiere([FromRoute] int Id)
        {
            var biere = await _biereRepository.GetById(Id);

            return Ok(biere);
        }
        [HttpPost]
        public async Task<ActionResult<Biere>> AddBiere(CreateBiereCommand command)
        {
            var biere = await _biereRepository.Add(command);

            if (biere == null) { return NotFound(); }

            return Ok(biere);
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete([FromRoute] int Id)
        {
            await _biereRepository.Delete(Id);

            return Ok($"User avec {Id} a été correctement supprimer");
        }
    }
}
