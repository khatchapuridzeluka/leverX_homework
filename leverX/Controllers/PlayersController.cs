using leverX.Domain.Enums;
using leverX.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace leverX.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase 
    {
        private static List<Player> Players = new List<Player>
        {
            new Player { Id = Guid.NewGuid(), Name = "Magnus", LastName = "Carlsen", Nationality = Nationality.Norway, Sex = Sex.Male, FideRating = 2861, Title = Title.GM, GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = Guid.NewGuid(), Name = "Hikaru", LastName = "Nakamura", Nationality = Nationality.USA, Sex = Sex.Male, FideRating = 2789, Title = Title.GM, GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = Guid.NewGuid(), Name = "Alireza", LastName = "Firouzja", Nationality = Nationality.France, Sex = Sex.Male, FideRating = 2757, Title = Title.GM, GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = Guid.NewGuid(), Name = "Ian", LastName = "Nepomniachtchi", Nationality = Nationality.Russia, Sex = Sex.Male, FideRating = 2771, Title = Title.GM, GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = Guid.NewGuid(), Name = "Ding", LastName = "Liren", Nationality = Nationality.China, Sex = Sex.Male, FideRating = 2780, Title = Title.GM, GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = Guid.NewGuid(), Name = "Anish", LastName = "Giri", Nationality = Nationality.Netherlands, Sex = Sex.Male, FideRating = 2764, Title = Title.GM, GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = Guid.NewGuid(), Name = "Fabiano", LastName = "Caruana", Nationality = Nationality.USA, Sex = Sex.Male, FideRating = 2782, Title = Title.GM, GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = Guid.NewGuid(), Name = "Wesley", LastName = "So", Nationality = Nationality.USA, Sex = Sex.Male, FideRating = 2769, Title = Title.GM, GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
            new Player { Id = Guid.NewGuid(), Name = "Teimour", LastName = "Radjabov", Nationality = Nationality.Azerbaijan, Sex = Sex.Male, FideRating = 2738, Title = Title.GM, GamesAsWhite = new List<Game>(), GamesAsBlack = new List<Game>() },
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
        public ActionResult<Player> GetPlayer(Guid id)
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
            player.Id = Guid.NewGuid();
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
        public ActionResult UpdatePlayer(Guid id, Player updatedPlayer)
        {
            var player = Players.FirstOrDefault(p => p.Id == id);
            if (player == null)
            {
                return NotFound();
            }
            player.Name = updatedPlayer.Name;
            player.LastName = updatedPlayer.LastName;
            player.Nationality = updatedPlayer.Nationality;
            player.Sex = updatedPlayer.Sex;
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
        public ActionResult DeletePlayer(Guid id)
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
