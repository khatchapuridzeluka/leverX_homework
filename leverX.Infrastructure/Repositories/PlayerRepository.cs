using System.Security.Cryptography.X509Certificates;
using leverX.Application.Interfaces.Repositories;
using leverX.Domain.Entities;
using leverX.DTOs.Players;
namespace leverX.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly List<Player> _players = new(); 

        public Task AddAsync(Player player)
        {   
            _players.Add(player);
            return Task.CompletedTask;
        }

        public Task<Player?> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_players.FirstOrDefault(p => p.Id == id));
        }
        public Task<List<Player>> GetAllAsync()
        {
            return Task.FromResult(_players.ToList());
        }

        public Task UpdateAsync(Player player)
        {
            var existing = _players.FirstOrDefault(p => p.Id == player.Id);
            if(existing != null)
            {
                existing.Name = player.Name;
                existing.LastName = player.LastName;
                existing.FideRating = player.FideRating;
                existing.Title = player.Title;
                existing.Nationality = player.Nationality;
                existing.Sex = player.Sex;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            _players.RemoveAll(p => p.Id == id);
            return Task.CompletedTask;
        }

        public Task<List<Player>> GetByRatingAsync(int rating)
        {
            var result = _players.Where(p => p.FideRating >= rating).ToList();
            return Task.FromResult(result);
        }
    }
}
    