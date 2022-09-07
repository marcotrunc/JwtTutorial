using JwtTutorial.SuperHeroesModel;
using JwtTutorial.UserModel;
using Microsoft.EntityFrameworkCore;



namespace JwtTutorial.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<SuperHero> SuperHeroes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Character> Characters { get; set; }
    }
}
