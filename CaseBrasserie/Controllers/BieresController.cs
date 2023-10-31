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
        public ActionResult<IEnumerable<Biere>> GetAll()
        {
            var bieres = _biereRepository.GetAll();

            return Ok(bieres);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Biere>> GetBiere([FromRoute] int Id)
        {
            var biere = await _biereRepository.GetById(Id);

            return Ok(biere);
        }
        [HttpPost]
        public ActionResult<Biere> AddBiere(CreateBiereCommand command)
        {
            var biere = _biereRepository.Add(command);

            if (biere == null) { return NotFound(); }

            return Ok(biere);
        }
        [HttpDelete("{Id}")]
        public ActionResult Delete([FromRoute] int Id)
        {
            var biereDelete = _biereRepository.Delete(Id);

            return Ok(biereDelete);
        }
    }
}
