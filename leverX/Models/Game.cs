using leverX.Models.Enums;

namespace leverX.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public Player? WhitePlayer { get; set; }
        public Player? BlackPlayer { get; set; }
        public Result Result { get; set; }
        public List<string> Moves { get; set; }
        public DateTime PlayedOn { get; set; }
        public Opening? Opening { get; set; }
    }
}
