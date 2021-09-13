using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter_API.Models
{
    public class CurrencyContext : DbContext
    {
        public CurrencyContext(DbContextOptions<CurrencyContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Rate> Rates { get; set; }
 
    }
}
