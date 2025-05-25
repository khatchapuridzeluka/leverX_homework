using leverX.Domain.Entities;
using leverX.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace leverX.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class  GamesController : ControllerBase 
    {
        private static List<Game> Games = new List<Game>
        {
            new Game { Id = Guid.NewGuid(),Result = (Result)1, Moves = new List<string> { "e4-e5", "Nf3-Nc6", "Bb5-a6" }},
            new Game { Id = Guid.NewGuid(),Result = (Result)2, Moves = new List<string> { "d4-d5", "c4-e6", "Nc3-Nf6" }},
            new Game { Id = Guid.NewGuid(),Result = (Result)3, Moves = new List<string> { "e4-c5", "Nf3-d6", "d4-cxd4" }},
            new Game { Id = Guid.NewGuid(),Result = (Result)1, Moves = new List<string> { "e4-e5", "Nf3-Nc6", "Bc4-Bc5" }},
            new Game { Id = Guid.NewGuid(),Result = (Result)2, Moves = new List<string> { "d4-Nf6", "c4-g6", "Nc3-Bg7" }},
            new Game { Id = Guid.NewGuid(),Result = (Result)1, Moves = new List<string> { "e4-e6", "d4-d5", "Nc3-Bb4" }},
            new Game { Id = Guid.NewGuid(),Result = (Result)3, Moves = new List<string> { "e4-c5", "Nf3-Nc6", "d4-cxd4" }},
            new Game { Id = Guid.NewGuid(),Result = (Result)1, Moves = new List<string> { "d4-d5", "c4-c6", "Nc3-Nf6" }},
            new Game { Id = Guid.NewGuid(),Result = (Result)2, Moves = new List<string> { "e4-c5", "Nf3-d6", "d4-cxd4" }},
            new Game { Id = Guid.NewGuid(),Result = (Result)1, Moves = new List<string> { "e4-e5", "Nf3-Nc6", "d4-exd4" }}
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
        public ActionResult<Game> GetGame(Guid id)
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
            game.Id = Guid.NewGuid();
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
        public ActionResult<Game> UpdateGame(Guid id, Game updatedGame)
        {
            var game = Games.FirstOrDefault(g => g.Id == id);
            if ( game == null)
            {
                return NotFound();
            }
            game.Result = updatedGame.Result;
            game.Moves = updatedGame.Moves;
            game.PlayedOn = updatedGame.PlayedOn;
            return NoContent();
        }

        /// <summary>
        /// Delete a Game
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult DeleteGame(Guid id)
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