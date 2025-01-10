using Microsoft.EntityFrameworkCore;
using Yacaba.Core.Exceptions;
using Yacaba.Domain.Models;
using Yacaba.Domain.Requests;
using Yacaba.Domain.Stores;

namespace Yacaba.EntityFramework.Stores {
    public class EntityFrameworkGymStore : IGymStore {

        private readonly ApplicationDbContext _context;

        public EntityFrameworkGymStore(
            ApplicationDbContext context
        ) {
            _context = context;
        }

        public Task<Gym?> GetByIdAsync(Int64 id, CancellationToken cancellationToken = default) {
            return _context.Gyms.Where(p => p.Id == id)
                .Include(p => p.Organisation)
                .SingleOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public IQueryable<Gym> GetAll() => _context.Gyms;
        
        public Task<Gym> CreateAsync(GymCreateRequest request, CancellationToken cancellationToken = default) {
            var newItem = new Gym {
                Name = request.Name,
                Image = request.Image,
                IsOffical = false
            };

            _context.Gyms.Add(newItem);

            return Task.FromResult(newItem);
        }
        public async Task UpdateAsync(GymUpdateRequest request, CancellationToken cancellationToken = default) {
            Gym existing = await GetByIdAsync(request.Id, cancellationToken: cancellationToken).ConfigureAwait(false) ?? throw new EntityNotFoundExeption(nameof(Gym), request.Id);

            existing.Name = request.Name;
            existing.Image = request.Image;
        }
        public async Task DeleteAsync(Int64 id, CancellationToken cancellationToken = default) {
            Gym existing = await GetByIdAsync(id, cancellationToken: cancellationToken).ConfigureAwait(false) ?? throw new EntityNotFoundExeption(nameof(Gym), id);
            _context.Gyms.Remove(existing);
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default) => _context.SaveChangesAsync(cancellationToken: cancellationToken);

    }
}
