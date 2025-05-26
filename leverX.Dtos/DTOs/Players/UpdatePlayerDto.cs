using leverX.Domain.Enums;
using leverX.Dtos.DTOs.Players;

namespace leverX.DTOs.Players
{
    public class UpdatePlayerDto : BasePlayerDto
    {
        public Guid Id { get; set; }
    }

}
