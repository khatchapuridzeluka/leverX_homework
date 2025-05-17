namespace leverX.Models
{
    public class Game
    {
        public int Id { get; set; }

        public int WhitePlayerId { get; set; }
        public Player WhitePlayer { get; set; }

        public int BlackPlayerId { get; set; }
        public Player BlackPlayer { get; set; }

        public string Result { get; set; }

        public List<string> Moves { get; set; }
        public DateTime PlayedOn { get; set; }

        public int OpeningId { get; set; }
        public Opening? Opening { get; set; }
    }

}
