using Microsoft.AspNetCore.Mvc;
using leverX.Models;

namespace leverx.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TournamentsController : ControllerBase
    {
        private static List<Tournament> Tournaments = new List<Tournament>
        {
            new Tournament
            {
                Id = Guid.NewGuid(),
                Name = "World Chess Championship 2024",
                StartDate = new DateTime(2024, 4, 1),
                EndDate = new DateTime(2024, 4, 30),
                Location = "Dubai, UAE",
                Players = new List<Player>()
            },
            new Tournament
            {
                Id = Guid.NewGuid(),
                Name = "Candidates Tournament 2023",
                StartDate = new DateTime(2023, 6, 5),
                EndDate = new DateTime(2023, 6, 25),
                Location = "Madrid, Spain",
                Players = new List<Player>()
            },
            new Tournament
            {
                Id = Guid.NewGuid(),
                Name = "Tata Steel Chess 2024",
                StartDate = new DateTime(2024, 1, 13),
                EndDate = new DateTime(2024, 1, 28),
                Location = "Wijk aan Zee, Netherlands",
                Players = new List<Player>()
            },
            new Tournament
            {
                Id = Guid.NewGuid(),
                Name = "Grand Chess Tour Romania",
                StartDate = new DateTime(2024, 5, 9),
                EndDate = new DateTime(2024, 5, 19),
                Location = "Bucharest, Romania",
                Players = new List<Player>()
            },
            new Tournament
            {
                Id = Guid.NewGuid(),
                Name = "Sinquefield Cup 2023",
                StartDate = new DateTime(2023, 8, 15),
                EndDate = new DateTime(2023, 8, 29),
                Location = "Saint Louis, USA",
                Players = new List<Player>()
            }
        };

        /// <summary>
        /// Get all tournaments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Tournament>> GetTournaments()
        {
            return Ok(Tournaments);
        }

        /// <summary>
        /// Get Tournament by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Tournament> GetTournament(Guid id)
        {
            var tournament = Tournaments.FirstOrDefault(t => t.Id == id);
            if (tournament == null)
            {
                return NotFound();
            }
            return Ok(tournament);
        }

        /// <summary>
        /// Create a new tournament
        /// </summary>
        /// <param name="tournament"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Tournament> CreateTournament(Tournament tournament)
        {
            tournament.Id = Guid.NewGuid();
            tournament.Players = new List<Player>();
            Tournaments.Add(tournament);
            return CreatedAtAction(nameof(GetTournament), new { id = tournament.Id }, tournament);
        }

        /// <summary>
        /// Update the tournament
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedTournament"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult UpdateTournament(Guid id, Tournament updatedTournament)
        {
            var tournament = Tournaments.FirstOrDefault(t => t.Id == id);
            if (tournament == null)
            {
                return NotFound();
            }
            tournament.Name = updatedTournament.Name;
            tournament.StartDate = updatedTournament.StartDate;
            tournament.EndDate = updatedTournament.EndDate;
            tournament.Location = updatedTournament.Location;
            return NoContent();
        }

        /// <summary>
        /// Delete the tournament
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteTournament(Guid id)
        {
            var tournament = Tournaments.FirstOrDefault(t => t.Id == id);
            if(tournament == null)
            {
                return NotFound();
            }
            Tournaments.Remove(tournament);
            return NoContent();
        }
    }
}