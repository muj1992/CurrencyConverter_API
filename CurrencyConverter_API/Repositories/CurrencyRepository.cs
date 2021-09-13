using CurrencyConverter_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter_API.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly CurrencyContext _context; 

       public CurrencyRepository(CurrencyContext context)
        {
            _context = context;

        }

        public async Task<Currency> Get(string code)
        {
           var query = await _context.Currencies
                           .Where(x => x.BaseCurrencyCode == code)
                           .Join(_context.Rates,
                                  curr => curr.Id,
                                  ra => ra.CurrencyId,
                                  (curr, ra) => new Currency
                                  {
                                      Id = curr.Id,
                                      BaseCurrencyCode = curr.BaseCurrencyCode,
                                      Rates = _context.Rates.Where(x => x.CurrencyId == curr.Id).ToList()
                                  }

                               )
                           .FirstOrDefaultAsync();


                return query;
     
        } 

        public async Task<List<string>> GetCountries()
        {
           return await _context.Currencies.Select(x => x.BaseCurrencyCode).ToListAsync();

        }

       public async Task<Currency> Create(Currency currency)
        {
             _context.Add(currency);

            await _context.SaveChangesAsync();

            return currency;
            
        }

        public async Task<ExchangeRate> CalculateExchange(ExchangeRate exchangeRate)
        {

            var currency = await Get(exchangeRate.FromCode);

            var rate = currency.Rates.Where(x => x.Currency == exchangeRate.ToCode)
                                     .Select(x => x.Value)
                                     .FirstOrDefault();

            if (currency != null && rate > 0)
            {
                exchangeRate.RateOfExchange = rate;
                exchangeRate.OutputAmount = Int32.Parse(exchangeRate.InputAmount) * rate;

            }

            return exchangeRate;          
        }
    }
}
