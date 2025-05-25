using leverX.Domain.Enums;

namespace leverX.DTOs.Players
{
    public class PlayerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Sex Sex { get; set; }
        public Nationality Nationality { get; set; }
        public int FideRating { get; set; }
        public Title Title { get; set; }
        public List<Guid> GamesAsWhite { get; set; }
        public List<Guid> GamesAsBlack { get; set; }
        public int GamesAsBlackCount { get; set; }
        public int TournamentsCount { get; set; }
    }

}
