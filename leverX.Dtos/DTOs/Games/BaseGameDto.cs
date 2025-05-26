using leverX.Domain.Enums;

namespace leverX.Dtos.DTOs.Games
{
    public class BaseGameDto
    {
        public Guid WhitePlayerId { get; set; }
        public Guid BlackPlayerId { get; set; }
        public Result Result { get; set; }
        public List<string> Moves { get; set; } = new();
        public DateTime PlayedOn { get; set; }
        public Guid OpeningId { get; set; }
        public Guid? TournamentId { get; set; }
    }
}
