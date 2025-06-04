using System.Data;
using Dapper;
using leverX.Application.Helpers.Constants;
using leverX.Domain.Exceptions;
using leverX.Application.Interfaces.Repositories;
using leverX.Domain.Entities;

namespace leverX.Infrastructure.Repositories
{
    public class TournamentPlayerRepository : ITournamentPlayerRepository
    {
        private readonly IDbConnection _tournamentPlayers;

        public TournamentPlayerRepository(IDbConnection tournamentPlayers)
        {
            _tournamentPlayers = tournamentPlayers;
        }

        //before - return _tournamentPlayers.ExecuteAsync(sql, parameters);
        //after - added async/await - insert query without blocking
        public async Task AddAsync(TournamentPlayer entity)
        {
            var sql = @"
                INSERT INTO TournamentPlayers (TournamentId, PlayerId, FinalRank, Score)
                VALUES (@TournamentId, @PlayerId, @FinalRank, @Score)";
            var parameters = new
            {
                entity.TournamentId,
                entity.PlayerId,
                entity.FinalRank,
                entity.Score
            };
            int affectedRows = await _tournamentPlayers.ExecuteAsync(sql, parameters);
            if (affectedRows == 0)
            {
                throw new InsertFailedException(ExceptionMessages.InsertFailed);
            }
        }

        // Executes select query to get tournament player by tournamentId and playerId without blocking
        public async Task<TournamentPlayer?> GetByIdAsync(Guid tournamentId, Guid playerId)
        {
            var sql = @"
                SELECT * FROM TournamentPlayers 
                WHERE TournamentId = @TournamentId AND PlayerId = @PlayerId";

            var tp = await _tournamentPlayers.QueryFirstOrDefaultAsync<TournamentPlayer>(
                sql,
                new { TournamentId = tournamentId, PlayerId = playerId });

            return tp;
        }

        // Executes select query to get all tournament players without blocking
        public async Task<IEnumerable<TournamentPlayer>> GetAllAsync()
        {
            var sql = "SELECT * FROM TournamentPlayer";
            var tournamentPlayers = await _tournamentPlayers.QueryAsync<TournamentPlayer>(sql);
            return tournamentPlayers.AsList();
        }

        // Executes update query to update tournament player without blocking
        public async Task UpdateAsync(TournamentPlayer entity)
        {
            var sql = @"
                UPDATE TournamentPlayers
                SET FinalRank = @FinalRank, Score = @Score
                WHERE TournamentId = @TournamentId AND PlayerId = @PlayerId";
            var parameters = new
            {
                entity.FinalRank,
                entity.Score,
                entity.TournamentId,
                entity.PlayerId
            };
            int affectedRows = await _tournamentPlayers.ExecuteAsync(sql, parameters);

            if ( affectedRows == 0 )
            {
                throw new NotFoundException(ExceptionMessages.TournamentPlayerNotFound);
            }
        }
        // Executes delete query to remove tournament player by tournamentId and playerId without blocking
        public async Task DeleteAsync(Guid tournamentId, Guid playerId)
        {
            var sql = @"
                DELETE FROM TournamentPlayers 
                WHERE TournamentId = @TournamentId AND PlayerId = @PlayerId";
            int affectedRows = await _tournamentPlayers.ExecuteAsync(sql, new { TournamentId = tournamentId, PlayerId = playerId });

            if (affectedRows == 0)
            {
                throw new NotFoundException(ExceptionMessages.TournamentPlayerNotFound);
            }

        }
    }
}
