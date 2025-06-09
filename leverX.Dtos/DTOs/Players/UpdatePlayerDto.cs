using leverX.Domain.Enums;
using leverX.Dtos.DTOs.Players;

namespace leverX.DTOs.Players
{
    public record UpdatePlayerDto : BasePlayerDto
    {
        public Guid Id { get; init; }
    }
}
