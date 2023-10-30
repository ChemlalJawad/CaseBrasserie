using CaseBrasserie.Application.Repositories;
using CaseBrasserie.Application.Repositories.Commands.Grossistes;
using CaseBrasserie.Application.Repositories.Implementations;
using CaseBrasserie.Core.Entities;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<GrossisteBiere>> AddNewBiere(AddNewBiereCommand command)
        {
            await _grossisteRepository.AddNewBiereToGrosssite(command);

            return Ok(command);
        }

        [HttpPost("{stock}")]
        public async Task<ActionResult<GrossisteBiere>> UpdateStock(UpdateStockCommand command, [FromRoute] int stock)
        {
            command.Stock = stock;
            await _grossisteRepository.UpdateGrossisteBiere(command);

            return Ok(command);
        }

        [HttpPost]
        [Route("{grossisteId}/quotation")]
        public async Task<ActionResult<QuotationCommand>> GetQuotations(QuotationCommand command, [FromRoute] int grossisteId)
        {
            command.GrossisteId = grossisteId;
            var price = await _grossisteRepository.GetQuotation(command);
            command.PrixTotal = (double) price;
 
            return Ok(command);
        }
    }
}
