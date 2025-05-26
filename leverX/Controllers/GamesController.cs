using leverX.Application.Interfaces.Services;
using leverX.DTOs.Games;
using Microsoft.AspNetCore.Mvc;

namespace leverX.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        /// Get all games
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGames()
        {
            var games = await _gameService.GetAllAsync();
            return Ok(games);
        }

        /// <summary>
        /// Get a game by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGame(Guid id)
        {
            var game = await _gameService.GetByIdAsync(id);
            if (game == null)
                return NotFound();

            return Ok(game);
        }

        /// <summary>
        /// Create a new game
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<GameDto>> CreateGame(CreateGameDto dto)
        {
            var createdGame = await _gameService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetGame), new { id = createdGame.Id }, createdGame);
        }

        /// <summary>
        /// Update an existing game
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<GameDto>> UpdateGame(Guid id, UpdateGameDto dto)
        {
            var updatedGame = await _gameService.UpdateAsync(id, dto);
            return Ok(updatedGame); // Or NoContent() if you don't want to return anything
        }

        /// <summary>
        /// Delete a game
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(Guid id)
        {
            await _gameService.DeleteAsync(id);
            return NoContent();
        }
    }
}
