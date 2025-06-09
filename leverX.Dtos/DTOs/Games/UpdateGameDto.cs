using leverX.Dtos.DTOs.Games;
namespace leverX.DTOs.Games
{
    public record UpdateGameDto : BaseGameDto
    {
        public Guid Id { get; init; }
    }

}
