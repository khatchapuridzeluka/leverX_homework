namespace leverX.Domain.Entities
{
    public class Opening
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EcoCode { get; set; } 
        public List<string> Moves { get; set; }
    }
}
