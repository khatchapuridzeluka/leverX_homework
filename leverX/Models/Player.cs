﻿using leverX.Models.Enums;

namespace leverX.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public Sex Sex { get; set; }
        public Nationality Nationality { get; set; }

        public int FideRating { get; set; }

        public Title Title { get; set; } // GM, IM, FM, etc.
        public ICollection<Game> GamesAsWhite { get; set; } = new List<Game>(); 
        public ICollection<Game> GamesAsBlack { get; set; } = new List<Game>();
    }
}
