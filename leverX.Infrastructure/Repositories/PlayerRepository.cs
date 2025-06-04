using System.Data;
using leverX.Application.Helpers.Constants;
using leverX.Domain.Exceptions;
using Dapper;
using leverX.Application.Interfaces.Repositories;
using leverX.Domain.Entities;
namespace leverX.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IDbConnection _players;

        public PlayerRepository(IDbConnection players)
        {
            _players = players;
        }

        //before - return _players.ExecuteAsync(sql, parameters);
        //after - added async/await - insert query without blocking
        public async Task AddAsync(Player player)
        {
            var sql = @"INSERT INTO Players (Id, Name, LastName, Sex, Nationality, FideRating, Title)
                VALUES (@Id, @Name, @LastName, @Sex, @Nationality, @FideRating, @Title)";

            int affectedRows = await _players.ExecuteAsync(sql, player);

            if (affectedRows == 0)
            {
                throw new InsertFailedException(ExceptionMessages.InsertFailed);
            }
        }

        // Executes select query to get player by id without blocking
        public async Task<Player?> GetByIdAsync(Guid id)
        {
            var sql = @"SELECT * FROM Players WHERE Id = @Id";
            return await _players.QueryFirstOrDefaultAsync<Player>(sql, new { Id = id });
        }

        // Executes select query to get all players without blocking
        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            var sql = @"SELECT * FROM Players";
            var players = await _players.QueryAsync<Player>(sql);
            return players.ToList();
        }

        // Executes update query to update player without blocking
        public async Task UpdateAsync(Player player)
        {
            var sql = @"UPDATE Players
                SET Name = @Name, LastName = @LastName, Sex = @Sex,
                    Nationality = @Nationality, FideRating = @FideRating, Title = @Title
                WHERE Id = @Id";

            int affectedRows = await _players.ExecuteAsync(sql, player);

            if (affectedRows == 0)
            {
                throw new NotFoundException(ExceptionMessages.PlayerNotFound);
            }
        }
        // Executes delete query to delete player by id without blocking
        public async Task DeleteAsync(Guid id)
        {
            var sql = @"DELETE FROM Players WHERE Id = @Id";
            int affectedRows = await _players.ExecuteAsync(sql, new { Id = id });

            if (affectedRows == 0)
            {
                throw new NotFoundException(ExceptionMessages.PlayerNotFound);
            }
        }

        // Executes select query to get players by rating without blocking
        public async Task<IEnumerable<Player>> GetByRatingAsync(int rating)
        {
            var sql = @"SELECT * FROM Players WHERE FideRating >= @Rating";

            var result = await _players.QueryAsync<Player>(sql, new { Rating = rating });
            return result.ToList();

        }
    }
}
    