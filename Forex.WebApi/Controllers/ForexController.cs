using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Forex.WebApi.Services;
using System;
using Microsoft.AspNetCore.Http;
 
namespace Forex.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForexController : ControllerBase
    {
        private readonly IExchangeMarket _exchangeMarket;

        public ForexController(IExchangeMarket exchangeMarket)
        {
            _exchangeMarket = exchangeMarket;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var currencies = await _exchangeMarket.GetSupportedCurrenciesAsync();
                return Ok(currencies);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get supported currencies");
            }
        }
    }
}