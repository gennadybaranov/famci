namespace GameForum.DomainModel.Domain
{
    public class Game
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int GenreId { get; set; }
        public string Genre { get; set; }
        public int AgeRestriction { get; set; }
    }
}
