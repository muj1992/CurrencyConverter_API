
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter_API.Models
{
    public class ExchangeRate
    {
        public string FromCode { get; set; }
        public string ToCode { get; set; }

        public string InputAmount { get; set; }

        public double OutputAmount { get; set; }

        public double RateOfExchange { get; set; }

     

    }
}
