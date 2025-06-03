using leverX.Domain.Enums;

namespace leverX.Dtos.DTOs.Games
{
    public record BaseGameDto
    {
        public Guid WhitePlayerId { get; init; }
        public Guid BlackPlayerId { get; init; }
        public Result Result { get; init; }
        public List<string> Moves { get; init; } = new();
        public DateTime PlayedOn { get; init; }
        public Guid OpeningId { get; init; }
        public Guid? TournamentId { get; init; }
    }
}
