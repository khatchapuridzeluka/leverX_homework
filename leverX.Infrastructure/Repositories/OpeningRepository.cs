using System.Data;
using System.Text.Json;
using Dapper;
using leverX.Application.Interfaces.Repositories;
using leverX.Domain.Entities;

namespace leverX.Infrastructure.Repositories
{
    public class OpeningRepository : IOpeningRepository
    {
        private readonly IDbConnection _connection;

        public OpeningRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public Task AddAsync(Opening entity)
        {
            var sql = @"INSERT INTO Openings (Id, Name, EcoCode, Moves)
                        VALUES (@Id, @Name, @EcoCode, @Moves)";
            var parameters = new
            {
                entity.Id,
                entity.Name,
                entity.EcoCode,
                Moves = JsonSerializer.Serialize(entity.Moves)
            };
            return _connection.ExecuteAsync(sql, parameters);
        }

        public async Task<Opening?> GetByIdAsync(Guid id)
        {
            var sql = @"SELECT * FROM Openings WHERE Id = @Id";
            var row = await _connection.QueryFirstOrDefaultAsync<OpeningDbRow>(sql, new { Id = id });
            if (row == null) return null;

            return new Opening
            {
                Id = row.Id,
                Name = row.Name,
                EcoCode = row.EcoCode,
                Moves = JsonSerializer.Deserialize<List<string>>(row.Moves) ?? new List<string>()
            };
        }

        public async Task<IEnumerable<Opening>> GetAllAsync()
        {
            var sql = @"SELECT * FROM Openings";

            var rows = await _connection.QueryAsync<OpeningDbRow>(sql);
            return rows.Select(row => new Opening
            {
                Id = row.Id,
                Name = row.Name,
                EcoCode = row.EcoCode,
                Moves = JsonSerializer.Deserialize<List<string>>(row.Moves) ?? new List<string>()
            }).ToList();
        }

        public Task UpdateAsync(Opening entity)
        {
            var sql = @"UPDATE Openings
                        SET Name = @Name, EcoCode = @EcoCode, Moves = @Moves
                        WHERE Id = @Id";
            var parameters = new
            {
                entity.Id,
                entity.Name,
                entity.EcoCode,
                Moves = JsonSerializer.Serialize(entity.Moves)
            };
            return _connection.ExecuteAsync(sql, parameters);
        }

        public Task DeleteAsync(Guid id)
        {
            var sql = "DELETE FROM Openings WHERE Id = @Id";
            return _connection.ExecuteAsync(sql, new { Id = id });
        }

        private class OpeningDbRow
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string EcoCode { get; set; } = string.Empty;
            public string Moves { get; set; } = "[]";
        }
    }
}
