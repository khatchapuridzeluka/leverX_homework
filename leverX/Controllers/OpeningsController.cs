using leverX.Models;
using Microsoft.AspNetCore.Mvc;

namespace leverX.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpeningsController : ControllerBase
    {
        private static List<Opening> Openings = new List<Opening>
        {
            new Opening { Id = 1, Name = "Ruy Lopez", EcoCode = "C60", Moves = "e4 e5 Nf3 Nc6 Bb5" },
            new Opening { Id = 2, Name = "Queen's Gambit Declined", EcoCode = "D30", Moves = "d4 d5 c4 e6 Nc3 Nf6" },
            new Opening { Id = 3, Name = "Sicilian Defense", EcoCode = "B20", Moves = "e4 c5" },
            new Opening { Id = 4, Name = "Italian Game", EcoCode = "C50", Moves = "e4 e5 Nf3 Nc6 Bc4" },
            new Opening { Id = 5, Name = "King's Indian Defense", EcoCode = "E60", Moves = "d4 Nf6 c4 g6 Nc3 Bg7" },
            new Opening { Id = 6, Name = "French Defense: Winawer", EcoCode = "C15", Moves = "e4 e6 d4 d5 Nc3 Bb4" },
            new Opening { Id = 7, Name = "Slav Defense", EcoCode = "D10", Moves = "d4 d5 c4 c6 Nc3 Nf6" },
            new Opening { Id = 8, Name = "Scotch Game", EcoCode = "C45", Moves = "e4 e5 Nf3 Nc6 d4 exd4" }
        };

        /// <summary>
        /// Get all openings
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Opening>> GetOpenings()
        {
            return Ok(Openings);
        }

        /// <summary>
        /// Get Opening by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Opening> GetOpening(int id)
        {
            var opening = Openings.FirstOrDefault(o => o.Id == id);
            if (opening == null)
            {
                return NotFound();
            }
            return Ok(opening);
        }

        /// <summary>
        /// Create a new opening
        /// </summary>
        /// <param name="opening"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Opening> CreateOpening(Opening opening)
        {
            opening.Id = Openings.Max(o => o.Id) + 1;
            Openings.Add(opening);
            return CreatedAtAction(nameof(GetOpening), new { id = opening.Id }, opening);
        }

        /// <summary>
        /// Update the opening
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedOpening"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult UpdateOpening(int id, Opening updatedOpening)
        {
            var opening = Openings.FirstOrDefault(o => o.Id == id);
            if (opening == null)
            {
                return NotFound();
            }
            opening.Name = updatedOpening.Name;
            opening.EcoCode = updatedOpening.EcoCode;
            opening.Moves = updatedOpening.Moves;
            return NoContent();
        }

        /// <summary>
        /// Delete the opening
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteOpening(int id)
        {
            var opening = Openings.FirstOrDefault(o => o.Id == id);
            if (opening == null)
            {
                return NotFound();
            }
            Openings.Remove(opening);
            return NoContent();
        }
    }
}
