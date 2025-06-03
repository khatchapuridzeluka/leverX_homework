namespace leverX.Dtos.DTOs.Openings
{
    public record BaseOpeningDto
    {
        public string Name { get; set; }
        public string EcoCode { get; set; }
        public List<string> Moves { get; set; }
    }
}
