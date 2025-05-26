using leverX.Application.Interfaces.Repositories;
using leverX.Domain.Entities;

namespace leverX.Infrastructure.Repositories
{
    public class OpeningRepository : IOpeningRepository
    {
        private readonly List<Opening> _openings = new();
        public Task AddAsync(Opening opening)
        {
            _openings.Add(opening);
            return Task.CompletedTask;
        }

        public Task<Opening?> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_openings.FirstOrDefault(o => o.Id == id));
        }

        public Task<List<Opening>> GetAllAsync()
        {
            return Task.FromResult(_openings.ToList());
        }

        public Task UpdateAsync(Opening opening)
        {
            var existing = _openings.FirstOrDefault(o => o.Id == opening.Id);
            if (existing != null)
            {
                existing.Name = opening.Name;
                existing.EcoCode = opening.EcoCode;
                existing.Moves = opening.Moves;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            _openings.RemoveAll(o => o.Id == id);
            return Task.CompletedTask;
        }

    }
}
