using Microsoft.EntityFrameworkCore;
using Yacaba.Core.Exceptions;
using Yacaba.Domain.Models;
using Yacaba.Domain.Requests;
using Yacaba.Domain.Stores;

namespace Yacaba.EntityFramework.Stores {
    public class EntityFrameworkWallStore : IWallStore {

        private readonly ApplicationDbContext _context;

        public EntityFrameworkWallStore(
            ApplicationDbContext context
        ) {
            _context = context;
        }

        public Task<Wall?> GetByIdAsync(Int64 id, CancellationToken cancellationToken = default) {
            return _context.Walls.Where(p => p.Id == id)
                .Include(p => p.Gym).ThenInclude(p => p.Organisation)
                .SingleOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public IQueryable<Wall> GetAll() => _context.Walls;

        public async Task<Wall> CreateAsync(WallCreateRequest request, CancellationToken cancellationToken = default) {
            var gym = await _context.Gyms.FindAsync([request.Gym], cancellationToken: cancellationToken).ConfigureAwait(false) ?? throw new InvalidDataException($"Cannot find a valig gym with id : {request.Gym}");

            var newItem = new Wall {
                Name = request.Name,
                Image = request.Image,
                WallType = request.WallType,
                Angle = request.Angle,
                Gym = gym
            };

            _context.Walls.Add(newItem);

            return newItem;
        }
        public async Task UpdateAsync(WallUpdateRequest request, CancellationToken cancellationToken = default) {
            Wall existing = await GetByIdAsync(request.Id, cancellationToken: cancellationToken).ConfigureAwait(false) ?? throw new EntityNotFoundExeption(nameof(Wall), request.Id);

            existing.Name = request.Name;
            existing.Image = request.Image;
        }
        public async Task DeleteAsync(Int64 id, CancellationToken cancellationToken = default) {
            Wall existing = await GetByIdAsync(id, cancellationToken: cancellationToken).ConfigureAwait(false) ?? throw new EntityNotFoundExeption(nameof(Wall), id);
            _context.Walls.Remove(existing);
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default) => _context.SaveChangesAsync(cancellationToken: cancellationToken);

    }
}
