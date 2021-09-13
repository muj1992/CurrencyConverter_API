using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter_API.Models
{
    [Table("Rates")]
    public class Rate
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Currency { get; set; }

        public double Value { get; set; }

        [ForeignKey(typeof(Currency))]
        public int CurrencyId { get; set; }
    }
}
