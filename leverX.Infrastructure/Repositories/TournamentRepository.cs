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
        var sql = @"INSERT INTO Tournaments (Id, Name, StartDate, EndDate, Location)
                    VALUES (@Id, @Name, @StartDate, @EndDate, @Location)";

        var parameters = new
        {
            tournament.Id,
            tournament.Name,
            tournament.StartDate,
            tournament.EndDate,
            tournament.Location
        };

        return _dbConnection.ExecuteAsync(sql, parameters);
    }

    public Task<Tournament?> GetByIdAsync(Guid id)
    {
        var sql = "SELECT * FROM Tournaments WHERE Id = @Id";
        return _dbConnection.QueryFirstOrDefaultAsync<Tournament>(sql, new { Id = id });
    }

    public async Task<IEnumerable<Tournament>> GetAllAsync()
    {
        var sql = "SELECT * FROM Tournaments";
        var rows = await _dbConnection.QueryAsync<Tournament>(sql);
        return rows.ToList();
    }

    public Task UpdateAsync(Tournament tournament)
    {
        var sql = @"UPDATE Tournaments
                    SET Name = @Name, StartDate = @StartDate, EndDate = @EndDate, Location = @Location
                    WHERE Id = @Id";

        var parameters = new
        {
            tournament.Id,
            tournament.Name,
            tournament.StartDate,
            tournament.EndDate,
            tournament.Location
        };

        return _dbConnection.ExecuteAsync(sql, parameters);
    }

    public Task DeleteAsync(Guid id)
    {
        var sql = "DELETE FROM Tournaments WHERE Id = @Id";
        return _dbConnection.ExecuteAsync(sql, new { Id = id });
    }
}
