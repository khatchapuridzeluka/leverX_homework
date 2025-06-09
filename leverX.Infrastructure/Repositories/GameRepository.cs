using Dapper;
using leverX.Application.Helpers.Constants;
using leverX.Domain.Exceptions;
using leverX.Application.Interfaces.Repositories;
using leverX.Domain.Entities;
using leverX.Domain.Enums;
using System.Data;
using System.Text.Json;

namespace leverX.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly IDbConnection _games;

        public GameRepository(IDbConnection games)
        {
            _games = games;
        }

        public async Task AddAsync(Game game)
        {
            var sql = @"INSERT INTO Games
                (WhitePlayerId, BlackPlayerId, Result, Moves, PlayedOn, OpeningId, TournamentId)
                OUTPUT INSERTED.Id
                VALUES
                (@WhitePlayerId, @BlackPlayerId, @Result, @Moves, @PlayedOn, @OpeningId, @TournamentId)";

            var parameters = new
            {
                WhitePlayerId = game.WhitePlayer.Id,
                BlackPlayerId = game.BlackPlayer.Id,
                game.Result,
                Moves = JsonSerializer.Serialize(game.Moves),
                game.PlayedOn,
                OpeningId = game.Opening.Id,
                TournamentId = game.Tournament?.Id
            };

            var generatedId = await _games.QuerySingleAsync<Guid>(sql, parameters);
            game.Id = generatedId;
        

            int affectedRows = await _games.ExecuteAsync(sql, parameters);

            if( affectedRows == 0)
            {
                throw new InsertFailedException(ExceptionMessages.InsertFailed);
            }
        }

        public async Task<Game?> GetByIdAsync(Guid id)
        {
            var sql = "SELECT * FROM Games WHERE Id = @Id";
            var row = await _games.QueryFirstOrDefaultAsync<GameDbRow>(sql, new { Id = id });

            if (row == null)
                return null;

            return MapRowToGame(row);
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            var sql = "SELECT * FROM Games";
            var rows = await _games.QueryAsync<GameDbRow>(sql);

            return rows.Select(MapRowToGame).ToList();
        }

        public async Task UpdateAsync(Game game)
        {
            var sql = @"UPDATE Games SET
                            WhitePlayerId = @WhitePlayerId,
                            BlackPlayerId = @BlackPlayerId,
                            Result = @Result,
                            Moves = @Moves,
                            PlayedOn = @PlayedOn,
                            OpeningId = @OpeningId,
                            TournamentId = @TournamentId
                        WHERE Id = @Id";

            var parameters = new
            {
                game.Id,
                WhitePlayerId = game.WhitePlayer.Id,
                BlackPlayerId = game.BlackPlayer.Id,
                game.Result,
                Moves = JsonSerializer.Serialize(game.Moves),
                game.PlayedOn,
                OpeningId = game.Opening.Id,
                TournamentId = game.Tournament?.Id
            };

            int affectedRows = await _games.ExecuteAsync(sql, parameters);
            if ( affectedRows == 0)
            {
                throw new NotFoundException(ExceptionMessages.GameNotFound);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var sql = "DELETE FROM Games WHERE Id = @Id";

            int affectedRows = await _games.ExecuteAsync(sql, new { Id = id });

            if (affectedRows == 0)
            {
                throw new NotFoundException(ExceptionMessages.GameNotFound);
            }
        }

        private Game MapRowToGame(GameDbRow row)
        {
            return new Game
            {
                Id = row.Id,
                WhitePlayer = new Player { Id = row.WhitePlayerId },
                BlackPlayer = new Player { Id = row.BlackPlayerId },
                Result = row.Result,
                Moves = JsonSerializer.Deserialize<List<string>>(row.Moves) ?? new List<string>(),
                PlayedOn = row.PlayedOn,
                Opening = new Opening { Id = row.OpeningId },
                Tournament = row.TournamentId.HasValue ? new Tournament { Id = row.TournamentId.Value } : null
            };
        }

        private class GameDbRow
        {
            public Guid Id { get; set; }
            public Guid WhitePlayerId { get; set; }
            public Guid BlackPlayerId { get; set; }
            public Result Result { get; set; }
            public string Moves { get; set; } = string.Empty;
            public DateTime PlayedOn { get; set; }
            public Guid OpeningId { get; set; }
            public Guid? TournamentId { get; set; }
        }
    }
}
