namespace JwtTutorial.SuperHeroesModel
{
    public class UpdateCharacterDto
    {
        public string Name { get; set; } = "Character";
        public string RpgClass { get; set; } = "Knight";
        public int SuperHeroId { get; set; } = 1;
        public int Id { get; set; }
    }
}
