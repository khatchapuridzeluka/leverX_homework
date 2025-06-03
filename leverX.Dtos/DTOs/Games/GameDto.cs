using leverX.Dtos.DTOs.Games;

namespace leverX.DTOs.Games
{
    public record GameDto : BaseGameDto
    {
        public Guid Id { get; set; }
    }
}
