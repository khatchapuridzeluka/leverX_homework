namespace leverX.DTOs.TournamentPlayers
{
    public class TournamentPlayerDto
    {
        public Guid TournamentId { get; set; }
        public string TournamentName { get; set; }
        public Guid PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int FinalRank { get; set; }
        public float Score { get; set; }
    }

}
