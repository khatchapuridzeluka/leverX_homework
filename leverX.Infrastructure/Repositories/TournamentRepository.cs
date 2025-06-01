using Dapper;
using leverX.Application.Interfaces.Repositories;
using leverX.Domain.Entities;
using System.Data;
public class TournamentRepository : ITournamentRepository
{
    private readonly IDbConnection _dbConnection;

    public TournamentRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public Task AddAsync(Tournament tournament)
    {
        var sql = @"INSERT INTO Tournaments (Id, Name, StartDate, EndDate, Location, PlayersIds)
                    VALUES (@Id, @Name, @StartDate, @EndDate, @Location, @PlayersIds)";

        var parameters = new
        {
            tournament.Id,
            tournament.Name,
            tournament.StartDate,
            tournament.EndDate,
            tournament.Location,
            PlayersIds = string.Join(",", tournament.Players.Select(p => p.Id))
        };

        return _dbConnection.ExecuteAsync(sql, parameters);
    }

    public async Task<Tournament?> GetByIdAsync(Guid id)
    {
        var sql = "SELECT * FROM Tournaments WHERE Id = @Id";
        var row = await _dbConnection.QueryFirstOrDefaultAsync<TournamentDbRow>(sql, new { Id = id });

        if (row == null)
            return null;

        return MapToEntity(row);
    }

    public async Task<List<Tournament>> GetAllAsync()
    {
        var sql = "SELECT * FROM Tournaments";
        var rows = await _dbConnection.QueryAsync<TournamentDbRow>(sql);
        return rows.Select(MapToEntity).ToList();
    }

    public Task UpdateAsync(Tournament tournament)
    {
        var sql = @"UPDATE Tournaments
                    SET Name = @Name, StartDate = @StartDate, EndDate = @EndDate, Location = @Location, PlayersIds = @PlayersIds
                    WHERE Id = @Id";

        var parameters = new
        {
            tournament.Id,
            tournament.Name,
            tournament.StartDate,
            tournament.EndDate,
            tournament.Location,
            PlayersIds = string.Join(",", tournament.Players.Select(p => p.Id))
        };

        return _dbConnection.ExecuteAsync(sql, parameters);
    }

    public Task DeleteAsync(Guid id)
    {
        var sql = "DELETE FROM Tournaments WHERE Id = @Id";
        return _dbConnection.ExecuteAsync(sql, new { Id = id });
    }

    private static Tournament MapToEntity(TournamentDbRow row)
    {
        return new Tournament
        {
            Id = row.Id,
            Name = row.Name,
            StartDate = row.StartDate,
            EndDate = row.EndDate,
            Location = row.Location,
            Players = row.PlayersIds?.Split(',', StringSplitOptions.RemoveEmptyEntries)
                         .Select(id => new Player { Id = Guid.Parse(id) })
                         .ToList() ?? new List<Player>()
        };
    }
    private class TournamentDbRow
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public string? PlayersIds { get; set; }
    }
}
