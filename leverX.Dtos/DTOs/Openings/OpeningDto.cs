using leverX.Dtos.DTOs.Openings;

namespace leverX.DTOs.Openings
{
    public record OpeningDto : BaseOpeningDto
    {
        public Guid Id { get; init; }
    }

}
