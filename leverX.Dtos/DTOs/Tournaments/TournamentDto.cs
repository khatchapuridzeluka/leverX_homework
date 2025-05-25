﻿namespace leverX.DTOs.Tournaments
{
    public class TournamentDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Location { get; set; }

        public List<Guid> PlayerIds { get; set; }
    }

}
