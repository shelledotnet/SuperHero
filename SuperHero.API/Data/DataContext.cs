using Microsoft.EntityFrameworkCore;
using SuperHero.API.Models;

namespace SuperHero.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }

        public DbSet<SuperHeroModel> tbl_superhero { get; set; }

    }
}
