namespace leverX.Domain.Entities
{
    public class TournamentPlayer
    {
        public Guid TournamentId { get; set; }
        public Guid PlayerId { get; set; }
        public int FinalRank { get; set; }
        public float Score { get; set; }
    }
}
