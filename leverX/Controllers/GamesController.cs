using leverX.Models;
using Microsoft.AspNetCore.Mvc;

namespace leverX.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class  GamesController : ControllerBase 
    {
        private static List<Game> Games = new List<Game>
        {
            new Game { Id = 1, WhitePlayerId = 1, BlackPlayerId = 2, Result = "1-0", Moves = new List<string> { "e4-e5", "Nf3-Nc6", "Bb5-a6" }, OpeningId = 1 },
            new Game { Id = 2, WhitePlayerId = 3, BlackPlayerId = 4, Result = "0-1", Moves = new List<string> { "d4-d5", "c4-e6", "Nc3-Nf6" }, OpeningId = 2 },
            new Game { Id = 3, WhitePlayerId = 5, BlackPlayerId = 6, Result = "1/2-1/2", Moves = new List<string> { "e4-c5", "Nf3-d6", "d4-cxd4" }, OpeningId = 3 },
            new Game { Id = 4, WhitePlayerId = 7, BlackPlayerId = 8, Result = "1-0", Moves = new List<string> { "e4-e5", "Nf3-Nc6", "Bc4-Bc5" }, OpeningId = 4 },
            new Game { Id = 5, WhitePlayerId = 9, BlackPlayerId = 10, Result = "0-1", Moves = new List<string> { "d4-Nf6", "c4-g6", "Nc3-Bg7" }, OpeningId = 5 },
            new Game { Id = 6, WhitePlayerId = 11, BlackPlayerId = 12, Result = "1-0", Moves = new List<string> { "e4-e6", "d4-d5", "Nc3-Bb4" }, OpeningId = 6 },
            new Game { Id = 7, WhitePlayerId = 13, BlackPlayerId = 14, Result = "1/2-1/2", Moves = new List<string> { "e4-c5", "Nf3-Nc6", "d4-cxd4" }, OpeningId = 3 },
            new Game { Id = 8, WhitePlayerId = 15, BlackPlayerId = 16, Result = "1-0", Moves = new List<string> { "d4-d5", "c4-c6", "Nc3-Nf6" }, OpeningId = 7 },
            new Game { Id = 9, WhitePlayerId = 17, BlackPlayerId = 18, Result = "0-1", Moves = new List<string> { "e4-c5", "Nf3-d6", "d4-cxd4" }, OpeningId = 3 },
            new Game { Id = 10, WhitePlayerId = 19, BlackPlayerId = 20, Result = "1-0", Moves = new List<string> { "e4-e5", "Nf3-Nc6", "d4-exd4" }, OpeningId = 8 }
        };

        /// <summary>
        /// Get All Games
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Game>> GetGames()
        {
            return Ok(Games);
        }


        /// <summary>
        /// Get Game by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Game> GetGame(int id)
        {
            var game = Games.FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }

        /// <summary>
        /// Create a new Game
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Game> CreateGame(Game game)
        {
            game.Id = Games.Max(g => g.Id) + 1;
            Games.Add(game);
            return CreatedAtAction(nameof(GetGame), new { id = game.Id }, game);
        }

        /// <summary>
        /// Update an existing Game
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedGame"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult<Game> UpdateGame(int id, Game updatedGame)
        {
            var game = Games.FirstOrDefault(g => g.Id == id);
            if ( game == null)
            {
                return NotFound();
            }
            game.WhitePlayerId = updatedGame.WhitePlayerId;
            game.BlackPlayerId = updatedGame.BlackPlayerId;
            game.Result = updatedGame.Result;
            game.Moves = updatedGame.Moves;
            game.PlayedOn = updatedGame.PlayedOn;
            game.OpeningId = updatedGame.OpeningId;
            return NoContent();
        }

        /// <summary>
        /// Delete a Game
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult DeleteGame(int id)
        {
            var game = Games.FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            Games.Remove(game);
            return NoContent();
        }
    };
 
}