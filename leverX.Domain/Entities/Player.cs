using leverX.Domain.Enums;

namespace leverX.Domain.Entities
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public Sex Sex { get; set; }
        public Nationality Nationality { get; set; }

        public int FideRating { get; set; }

        public Title Title { get; set; } // GM, IM, FM, etc.
        public ICollection<Game> GamesAsWhite { get; set; } = new List<Game>();
        public ICollection<Game> GamesAsBlack { get; set; } = new List<Game>();
        public ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
    }
}