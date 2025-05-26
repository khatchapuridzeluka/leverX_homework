
using leverX.Application.Interfaces.Services;
using leverX.DTOs.Tournaments;
using Microsoft.AspNetCore.Mvc;

namespace leverx.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TournamentsController : ControllerBase
    {

        private readonly ITournamentService _tournamentService;

        public TournamentsController(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }

        /// <summary>
        /// Get all tournaments
        /// </summary>
        [ProducesResponseType(typeof(IEnumerable<TournamentDto>),200)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDto>>> GetTournaments()
        {
            var tournaments = await _tournamentService.GetAllAsync();
            return Ok(tournaments);
        }

        /// <summary>
        /// Get Tournament by Id
        /// </summary>
        [ProducesResponseType(typeof(TournamentDto),200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDto>> GetTournament(Guid id)
        {
            var tournament = await _tournamentService.GetByIdAsync(id);

            if (tournament == null)
                return NotFound();

            return Ok(tournament);
        }

        /// <summary>
        /// Create a new tournament
        /// </summary>
        [ProducesResponseType(typeof(CreateTournamentDto),201)]
        [HttpPost]
        public async Task<ActionResult<TournamentDto>> CreateTournament(CreateTournamentDto dto)
        {
            var createdTournament = await _tournamentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetTournament), new { id = createdTournament.Id }, createdTournament);
        }

        /// <summary>
        /// Update the tournament
        /// </summary>
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTournament(Guid id, UpdateTournamentDto dto)
        {
            // TODO: Catch the excpetion
            await _tournamentService.UpdateAsync(id, dto);
            return NoContent();
        }

        /// <summary>
        /// Delete the tournament
        /// </summary>
        [ProducesResponseType(204)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTournament(Guid id)
        {
            await _tournamentService.DeleteAsync(id);
            return NoContent();
        }
    }
}