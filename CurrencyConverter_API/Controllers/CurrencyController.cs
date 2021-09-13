using CurrencyConverter_API.Models;
using CurrencyConverter_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyRepository _currencyRepository;
        public CurrencyController(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<Currency>> GetExchangeRateByCode(string code)
        {
            var exchanges = await _currencyRepository.Get(code);

            if (exchanges == null || exchanges.Rates.Count == 0)
                return NotFound();

            return Ok(exchanges);
        }

        [HttpGet("Countries")]
        public async Task<ActionResult<List<string>>> GetCountries()
        {

            return await _currencyRepository.GetCountries();
        }

        [HttpPost("{id}/Create")]
        public async Task<ActionResult<Currency>> PostExchangeRates([FromBody] Currency currency)
        {
            var newCurrency = await _currencyRepository.Create(currency);

            return Created(newCurrency.Id.ToString(), newCurrency);

        }

        [HttpPost] 
        public async Task<ActionResult<ExchangeRate>> CalculateExchangeRate([FromBody] ExchangeRate exchangeRate)
        {
            if (Int32.Parse(exchangeRate.InputAmount) <= 0)
                return BadRequest(new { error = "Please enter an amount to exchange." });

            if (exchangeRate.FromCode == exchangeRate.ToCode)
                return BadRequest(new { error = "Cannot convert from same currency of origin." });

            var exchangedRate = await _currencyRepository.CalculateExchange(exchangeRate);

            if (exchangedRate.RateOfExchange <= 0)
                return NotFound();

            return Ok(exchangedRate);
        }


    }
}
