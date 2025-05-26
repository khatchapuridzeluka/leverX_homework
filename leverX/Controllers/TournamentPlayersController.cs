using leverX.Application.Interfaces.Services;
using leverX.DTOs.TournamentPlayers;
using Microsoft.AspNetCore.Mvc;

namespace leverX.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TournamentPlayersController : ControllerBase
    {
        private readonly ITournamentPlayerService _tournamentPlayerService;

        public TournamentPlayersController(ITournamentPlayerService tournamentPlayerService)
        {
            _tournamentPlayerService = tournamentPlayerService;
        }

        /// <summary>
        /// Get All tournament Players
        /// </summary>
        [ProducesResponseType(typeof(IEnumerable<TournamentPlayerDto>), 200)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentPlayerDto>>> GetTournamentPlayers()
        {
            var tournamentPlayers = await _tournamentPlayerService.GetAllAsync();
            return Ok(tournamentPlayers);
        }

        /// <summary>
        /// get tournamentPlayer by Tournament Id and Player Id
        /// </summary>
        [ProducesResponseType(typeof(TournamentPlayerDto), 200)]
        [ProducesResponseType(404)]
        [HttpGet("tournament/{tournamentId}/player{playerId}")]
        public async Task<ActionResult<TournamentPlayerDto>> GetTournamentPlayer(Guid tournamentId, Guid playerId)
        {
            var tournamentPlayer =await _tournamentPlayerService.GetByIdAsync(tournamentId, playerId);
            if(tournamentPlayer == null)
                return NotFound();
            return Ok(tournamentPlayer);
        }

        /// <summary>
        /// Create a new tournament Player
        /// </summary>
        [ProducesResponseType(201)]
        [HttpPost]
        public async Task<ActionResult> CreateTournamentPlayer(CreateTournamentPlayerDto dto)
        {
            var createdTournamentPlayer = await _tournamentPlayerService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetTournamentPlayer), new {tournamentId = createdTournamentPlayer.TournamentId, playerId = createdTournamentPlayer.PlayerId}, createdTournamentPlayer);
        }

        /// <summary>
        /// Update an existing tournament player
        /// </summary>
        [ProducesResponseType(204)]
        [HttpPut("tournament/{tournamentId}/player/{playerId}")]
        public async Task<ActionResult> UpdateTournamentPlayer(Guid tournamentId, Guid playerId, UpdateTournamentPlayerDto dto)
        {
            await _tournamentPlayerService.UpdateAsync(tournamentId,playerId, dto);
            return NoContent();
        }

        /// <summary>
        /// Delete tournamentPlayer
        /// </summary>
        [ProducesResponseType(204)]
        [HttpDelete("tournament/{tournamentId}/player/{playerId}")]
        public async Task<ActionResult> DeleteTournamentPlayer(Guid tournamentId, Guid playerId)
        {
            await _tournamentPlayerService.DeleteAsync(tournamentId, playerId);
            return NoContent();
        }
    }
}
