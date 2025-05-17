using leverX.Models;
using Microsoft.AspNetCore.Mvc;

namespace leverX.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase 
    {
        private static List<Player> Players = new List<Player>
        {
            new Player { Id = 1, Name = "Magnus", LastName = "Carlsen", Nationality = "Norway", FideRating = 2861, Title = "GM", GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = 2, Name = "Hikaru", LastName = "Nakamura", Nationality = "USA", FideRating = 2789, Title = "GM", GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = 3, Name = "Alireza", LastName = "Firouzja", Nationality = "France", FideRating = 2757, Title = "GM", GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = 4, Name = "Ian", LastName = "Nepomniachtchi", Nationality = "Russia", FideRating = 2771, Title = "GM", GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = 5, Name = "Ding", LastName = "Liren", Nationality = "China", FideRating = 2780, Title = "GM", GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = 6, Name = "Anish", LastName = "Giri", Nationality = "Netherlands", FideRating = 2764, Title = "GM", GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = 7, Name = "Fabiano", LastName = "Caruana", Nationality = "USA", FideRating = 2782, Title = "GM", GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = 8, Name = "Wesley", LastName = "So", Nationality = "USA", FideRating = 2769, Title = "GM", GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = 9, Name = "Teimour", LastName = "Radjabov", Nationality = "Azerbaijan", FideRating = 2738, Title = "GM", GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = 10, Name = "Levon", LastName = "Aronian", Nationality = "USA", FideRating = 2754, Title = "GM", GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = 11, Name = "Maxime", LastName = "Vachier-Lagrave", Nationality = "France", FideRating = 2745, Title = "GM", GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = 12, Name = "Shakhriyar", LastName = "Mamedyarov", Nationality = "Azerbaijan", FideRating = 2747, Title = "GM", GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = 13, Name = "Sergey", LastName = "Karjakin", Nationality = "Russia", FideRating = 2723, Title = "GM", GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = 14, Name = "Richard", LastName = "Rapport", Nationality = "Romania", FideRating = 2735, Title = "GM", GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = 15, Name = "Vidit", LastName = "Gujrathi", Nationality = "India", FideRating = 2714, Title = "GM", GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = 16, Name = "Dommaraju", LastName = "Gukesh", Nationality = "India", FideRating = 2728, Title = "GM", GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = 17, Name = "Nodirbek", LastName = "Abdusattorov", Nationality = "Uzbekistan", FideRating = 2715, Title = "GM", GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = 18, Name = "Jan-Krzysztof", LastName = "Duda", Nationality = "Poland", FideRating = 2731, Title = "GM", GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = 19, Name = "Le", LastName = "Quang Liem", Nationality = "Vietnam", FideRating = 2709, Title = "GM", GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = 20, Name = "Bassem", LastName = "Amin", Nationality = "Egypt", FideRating = 2670, Title = "GM", GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() }
        };


        /// <summary>
        /// Get all players
        /// </summary>
        /// <returns>All Players</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Player>> GetPlayers()
        {
            return Ok(Players);
        }

        /// <summary>
        /// Get Player by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Player> GetPlayer(int id)
        {
            var player = Players.FirstOrDefault(p => p.Id == id);
            if (player == null)
            {
                return NotFound();

            }
            return Ok(player);
        }

        /// <summary>
        /// Add the Player
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Player> CreatePlayer(Player player)
        {
            player.Id = Players.Max(p => p.Id) + 1;
            player.GamesAsWhite = new List<Game>();
            player.GamesAsBlack = new List<Game>();
            Players.Add(player);
            return CreatedAtAction(nameof(GetPlayer), new { id = player.Id }, player);
        }


        /// <summary>
        /// Update the player
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedPlayer"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult UpdatePlayer(int id, Player updatedPlayer)
        {
            var player = Players.FirstOrDefault(p => p.Id == id);
            if (player == null)
            {
                return NotFound();
            }
            player.Name = updatedPlayer.Name;
            player.LastName = updatedPlayer.LastName;
            player.Nationality = updatedPlayer.Nationality;
            player.Title = updatedPlayer.Title;
            player.FideRating = updatedPlayer.FideRating;
            return NoContent();
        }

        /// <summary>
        /// Delete the player
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeletePlayer(int id)
        {
            var player = Players.FirstOrDefault(p => p.Id == id);

            if(player == null)
            {
                return NotFound();
            }
            Players.Remove(player);
            return NoContent();
        }
    }
}
