namespace leverX.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }

        public int FideRating { get; set; }

        public string? Title { get; set; } // GM, IM, FM, etc.
        public ICollection<Game> GamesAsWhite { get; set; } = new List<Game>(); 
        public ICollection<Game> GamesAsBlack { get; set; } = new List<Game>();
    }
}
