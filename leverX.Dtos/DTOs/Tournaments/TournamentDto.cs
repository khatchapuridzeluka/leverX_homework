using leverX.Dtos.DTOs.Tournaments;

namespace leverX.DTOs.Tournaments
{
    public record TournamentDto : BaseTournamentDto
    {
        public Guid Id { get; set; }
    }
}
