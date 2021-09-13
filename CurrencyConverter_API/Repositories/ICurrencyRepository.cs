using CurrencyConverter_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter_API.Repositories
{
    public interface ICurrencyRepository
    {
 
        Task<Currency> Get(string code);

        Task<List<string>> GetCountries();

        Task<Currency> Create(Currency currency);

        Task<ExchangeRate> CalculateExchange(ExchangeRate exchangeRate);


    }
}
