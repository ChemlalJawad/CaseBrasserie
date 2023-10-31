using CaseBrasserie.Application.Repositories;
using CaseBrasserie.Application.Repositories.Commands.Grossistes;
using CaseBrasserie.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CaseBrasserie.Web.Controllers
{
    [Route("api/grossistes")]
    [ApiController]
    public class GrossisteController : ControllerBase
    {
        private readonly IGrossisteRepository _grossisteRepository;
        public GrossisteController(IGrossisteRepository repository)
        {
            _grossisteRepository = repository;
        }

        [HttpPost]
        public ActionResult<GrossisteBiere> AddNewBiere(AddNewBiereCommand command)
        {
            var result = _grossisteRepository.AddNewBiereToGrosssite(command);

            return Ok(result);
        }

        [HttpPost("{stock}")]
        public ActionResult<GrossisteBiere> UpdateStock(UpdateStockCommand command, [FromRoute] int stock)
        {
            command.Stock = stock;
            var result = _grossisteRepository.UpdateGrossisteBiere(command);

            return Ok(result);
        }

        [HttpPost]
        [Route("{grossisteId}/quotation")]
        public ActionResult<QuotationCommand> GetQuotations(QuotationCommand command, [FromRoute] int grossisteId)
        {
            command.GrossisteId = grossisteId;
            var price = _grossisteRepository.GetQuotation(command);
            command.PrixTotal = (double)price;

            return Ok(command);
        }
    }
}
