using System.Text.Json.Serialization;

namespace JwtTutorial.SuperHeroesModel
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string RpgClass { get; set; } = "Knight";
        [JsonIgnore]
        public SuperHero SuperHero { get; set; } 
        public int SuperHeroId { get; set; }

        public Weapon Weapon { get; set; }
    }
}
