namespace leverX.DTOs.Openings
{
    public class CreateOpeningDto
    {
        public string Name { get; set; }
        public string EcoCode { get; set; }
        public List<string> Moves { get; set; }
    }
}
