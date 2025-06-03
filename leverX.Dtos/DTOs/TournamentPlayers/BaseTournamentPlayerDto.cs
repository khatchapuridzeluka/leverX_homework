namespace leverX.Dtos.DTOs.TournamentPlayers
{
    public record BaseTournamentPlayerDto
    {
        public Guid TournamentId { get; set; }
        public Guid PlayerId { get; set; }
        public int FinalRank { get; set; }
        public float Score { get; set; }
    }
}
