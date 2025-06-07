using leverX.Domain.Entities;
using leverX.DTOs.Games;
using leverX.DTOs.Openings;
using leverX.DTOs.Players;
using leverX.DTOs.TournamentPlayers;
using leverX.DTOs.Tournaments;

namespace leverX.Application.Helpers
{
    internal class DtoMapper
    {
        public static GameDto MapToDto(Game game) => new()
        {
            Id = game.Id,
            WhitePlayerId = game.WhitePlayer.Id,
            BlackPlayerId = game.BlackPlayer.Id,
            Result = game.Result,
            Moves = game.Moves,
            PlayedOn = game.PlayedOn,
            OpeningId = game.Opening.Id,
            TournamentId = game.Tournament != null ? game.Tournament.Id : (Guid?)null
        };

        public static OpeningDto MapToDto(Opening opening) => new()
        {
            Id = opening.Id,
            Name = opening.Name,
            EcoCode = opening.EcoCode,
            Moves = opening.Moves
        };

        public static PlayerDto MapToDto(Player p) => new()
        {
            Id = p.Id,
            Name = p.Name,
            LastName = p.LastName,
            Sex = p.Sex,
            Nationality = p.Nationality,
            FideRating = p.FideRating,
            Title = p.Title
        };

        public static TournamentPlayerDto MapToDto(TournamentPlayer tp) => new()
        {
            TournamentId = tp.TournamentId,
            PlayerId = tp.PlayerId,
            Point = tp.Point,
            FinalRank = tp.FinalRank

        };

        public static TournamentDto MapToDto(Tournament tournament) => new()
        {
            Id = tournament.Id,
            Name = tournament.Name,
            StartDate = tournament.StartDate,
            EndDate = tournament.EndDate,
            Location = tournament.Location
        };
    }
}
