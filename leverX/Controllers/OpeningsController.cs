using leverX.Application.Interfaces.Services;
using leverX.DTOs.Openings;
using Microsoft.AspNetCore.Mvc;

namespace leverX.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpeningsController : ControllerBase
    {
        private readonly IOpeningService _openingService;

        public OpeningsController(IOpeningService openingService)
        {
            _openingService = openingService;
        }

        /// <summary>
        /// Get all openings
        /// </summary>
        [ProducesResponseType(typeof(IEnumerable<OpeningDto>), 200)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OpeningDto>>> GetOpenings()
        {
            var openings = await _openingService.GetAllAsync();
            return Ok(openings);
        }

        /// <summary>
        /// Get Opening by Id
        /// </summary>
        [ProducesResponseType(typeof(OpeningDto), 200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<OpeningDto>> GetOpening(Guid id)
        {
            var opening = await _openingService.GetByIdAsync(id);
            if (opening == null)
            {
                return NotFound();
            }
            return Ok(opening);
        }

        /// <summary>
        /// Create a new opening
        /// </summary>
        [ProducesResponseType(typeof(OpeningDto),201)]
        [HttpPost]
        public async Task<ActionResult<OpeningDto>> CreateOpening(CreateOpeningDto dto)
        {
            var createdOpening = await _openingService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetOpening), new { id = createdOpening.Id }, createdOpening);
        }

        /// <summary>
        /// Update the opening
        /// </summary>
        [ProducesResponseType(typeof(UpdateOpeningDto), 204)]
        [ProducesResponseType(404)]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOpening(Guid id, UpdateOpeningDto dto)
        {
            //TODO: CATCH THE EXCEPTION
            await _openingService.UpdateAsync(id, dto);
            return NoContent();
        }

        /// <summary>
        /// Delete the opening
        /// </summary>
        [ProducesResponseType(204)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOpening(Guid id)
        {
            await _openingService.DeleteAsync(id);
            return NoContent();
        }
    }
}
