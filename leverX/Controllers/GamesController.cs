using leverX.Application.Interfaces.Services;
using leverX.Domain.Exceptions;
using leverX.DTOs.Games;
using Microsoft.AspNetCore.Authorization;
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
        [ProducesResponseType(typeof(IEnumerable<GameDto>), 200)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGames()
        {
            var games = await _gameService.GetAllAsync();
            return Ok(games);
        }

        /// <summary>
        /// Get a game by ID
        /// </summary>
        [ProducesResponseType(typeof(GameDto), 200)]
        [ProducesResponseType(404)]
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
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(GameDto), 201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpPost]
        public async Task<ActionResult<GameDto>> CreateGame(CreateGameDto dto)
        {
            try
            {
                var createdGame = await _gameService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetGame), new { id = createdGame.Id }, createdGame);
            }
            catch(NotFoundException)
            {
                // If any of the referenced entities (players, opening, tournament) are not found
                return NotFound("One or more fields from player was not found");
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Update an existing game
        /// </summary>
        [ProducesResponseType(typeof(GameDto), 204)]
        [ProducesResponseType(404)]
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GameDto>> UpdateGame(Guid id, UpdateGameDto dto)
        {
            try {
                await _gameService.UpdateAsync(id, dto);
            }
            catch(NotFoundException)
            {
                return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return NoContent();
        }

        /// <summary>
        /// Delete a game
        /// </summary>
        [ProducesResponseType(204)]
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGame(Guid id)
        {
            await _gameService.DeleteAsync(id);
            return NoContent();
        }
    }
}
