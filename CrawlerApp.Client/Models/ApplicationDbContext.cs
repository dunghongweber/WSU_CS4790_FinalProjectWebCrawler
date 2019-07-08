using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CrawlerApp.Client.Models
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<CrawlerLink> MyLinks { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> o)
            : base(o) { }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(LocalDB)\MSSQLLocalDB;Database=CS2550Tutor;Trusted_Connection=True;");
            }
        }
    }
}
