using leverX.Domain.Enums;

namespace leverX.DTOs.Games
{
    public class GameDto
    {
        public Guid Id { get; set; }

        public Guid WhitePlayerId { get; set; }
        public string WhitePlayerName { get; set; }

        public Guid BlackPlayerId { get; set; }
        public string BlackPlayerName { get; set; }

        public Result Result { get; set; }

        public List<string> Moves { get; set; } = new();

        public DateTime PlayedOn { get; set; }

        public string OpeningName { get; set; }
    }
}
