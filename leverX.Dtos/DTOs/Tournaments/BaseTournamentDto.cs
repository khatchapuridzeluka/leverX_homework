namespace leverX.Dtos.DTOs.Tournaments
{
    public class BaseTournamentDto
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public List<Guid> PlayerIds { get; set; }
    }
}
