using leverX.Dtos.DTOs.Openings;

namespace leverX.DTOs.Openings
{
    public record UpdateOpeningDto : BaseOpeningDto
    {
        public Guid Id { get; init; }
    }
}
