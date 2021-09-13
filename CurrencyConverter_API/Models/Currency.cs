
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter_API.Models
{
    [Table("Currencies")]
    public class Currency
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string BaseCurrencyCode { get; set; }

        public string Date { get { return DateTime.Now.Date.ToString(); } }

        public List<Rate> Rates { get; set; }

    }
}
