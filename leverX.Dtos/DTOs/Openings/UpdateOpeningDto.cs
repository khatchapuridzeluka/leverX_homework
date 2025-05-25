namespace leverX.DTOs.Openings
{
    public class UpdateOpeningDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string EcoCode { get; set; }

        public List<string> Moves { get; set; }
    }

}
