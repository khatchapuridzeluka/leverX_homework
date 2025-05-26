using Microsoft.AspNetCore.Mvc;
using leverX.Application.Interfaces.Services;
using leverX.DTOs.Players;

namespace leverX.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase 
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        /// <summary>
        /// Get all players
        /// </summary>
        [ProducesResponseType(typeof(IEnumerable<PlayerDto>), 200)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetPlayers()
        {
            var players = await _playerService.GetAllAsync();
            return Ok(players);
        }

        /// <summary>
        /// Get Player by Id
        /// </summary>
        [ProducesResponseType(typeof(PlayerDto), 200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerDto>> GetPlayer(Guid id)
        {
            var player = await _playerService.GetByIdAsync(id);
            if (player == null)
            {
                return NotFound();

            }
            return Ok(player);
        }

        /// <summary>
        /// Add the Player
        /// </summary>
        [ProducesResponseType(typeof(PlayerDto), 201)]
        [HttpPost]
        public async  Task<ActionResult<PlayerDto>> CreatePlayer(CreatePlayerDto dto)
        {
            var createdPlayer = await _playerService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetPlayer), new { id = createdPlayer.Id }, createdPlayer);
        }


        /// <summary>
        /// Update the player
        /// </summary>
        [ProducesResponseType(204)]
        [HttpPut("{id}")]
        public async  Task<ActionResult> UpdatePlayer(Guid id, UpdatePlayerDto dto)
        {
            await _playerService.UpdateAsync(id, dto);
            return NoContent();
        }

        /// <summary>
        /// Delete the player
        /// </summary>
        [ProducesResponseType(204)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlayer(Guid id)
        {
            await _playerService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Get players by rating
        /// </summary>
        [ProducesResponseType(typeof(IEnumerable<PlayerDto>),200)]
        [HttpGet("rating/{rating}")]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetByRating(int rating)
        {
            var players = await _playerService.GetByRatingAsync(rating);
            return Ok(players);
        }
    }
}
