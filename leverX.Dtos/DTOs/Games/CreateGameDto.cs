using leverX.Domain.Enums;

namespace leverX.DTOs.Games
{
    public class CreateGameDto
    {
        public Guid WhitePlayerId { get; set; }
        public Guid BlackPlayerId { get; set;}
        public Result Result { get; set; }
        public List<string> Moves { get; set; }
        public DateTime PlayedOn { get; set; }
        public Guid OpeningId { get; set; }

    }
}
