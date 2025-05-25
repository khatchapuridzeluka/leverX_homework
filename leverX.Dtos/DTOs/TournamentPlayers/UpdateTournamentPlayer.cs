namespace leverX.DTOs.TournamentPlayers
{
    public class UpdateTournamentPlayerDto
    {
        public Guid TournamentId { get; set; }
        public Guid PlayerId { get; set; }
        public int FinalRank { get; set; }
        public float Score { get; set; }
    }

}
