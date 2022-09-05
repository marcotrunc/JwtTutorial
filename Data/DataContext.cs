using JwtTutorial.SuperHeroesModel;
using Microsoft.EntityFrameworkCore;



namespace JwtTutorial.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<SuperHero> SuperHeroes { get; set; }
    }
}
