using leverX.Domain.Enums;

namespace leverX.Dtos.DTOs.Players
{
    public record BasePlayerDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }

        public Sex Sex { get; set; }
        public Nationality Nationality { get; set; }

        public int FideRating { get; set; }

        public Title Title { get; set; }
    }
}
