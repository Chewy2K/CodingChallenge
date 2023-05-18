using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesData.Models
{
    public class SalesDBContext : DbContext
    {
        public SalesDBContext() { }
        public SalesDBContext(DbContextOptions<SalesDBContext> options) : base(options) { }

        public DbSet<SalesModel> SalesData { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=SalesDB;Trusted_Connection=True;");
            }
        }
    }
}
