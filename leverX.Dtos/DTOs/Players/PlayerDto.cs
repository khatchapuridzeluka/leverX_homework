using leverX.Domain.Enums;
using leverX.Dtos.DTOs.Players;

namespace leverX.DTOs.Players
{
    public record PlayerDto : BasePlayerDto
    {
        public Guid Id { get; set; }
        public List<Guid> GamesAsWhite { get; set; }
        public List<Guid> GamesAsBlack { get; set; }
    }

}
