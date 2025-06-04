using leverX.Dtos.DTOs.Tournaments;

namespace leverX.DTOs.Tournaments
{
    public record UpdateTournamentDto : BaseTournamentDto
    {
        public Guid Id { get; set; }
    }

}
