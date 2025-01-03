using Microsoft.EntityFrameworkCore;
using Yacaba.Core.Exceptions;
using Yacaba.Domain.Models;
using Yacaba.Domain.Requests;
using Yacaba.Domain.Stores;

namespace Yacaba.EntityFramework.Stores {
    public class EntityframeworkOrganisationStore : IOrganisationStore {

        private readonly ApplicationDbContext _context;

        public EntityframeworkOrganisationStore(
            ApplicationDbContext context
        ) {
            _context = context;
        }

        public Task<Organisation?> GetByIdAsync(Int64 id, CancellationToken cancellationToken = default) {
            return _context.Organisations.Where(p => p.Id == id).SingleOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public IQueryable<Organisation> GetAll() => _context.Organisations;
        
        public Task<Organisation> CreateAsync(OrganisationCreateRequest request, CancellationToken cancellationToken = default) {
            var newItem = new Organisation {
                Name = request.Name,
                Image = request.Image,
                IsOffical = false
            };

            _context.Organisations.Add(newItem);

            return Task.FromResult(newItem);
        }
        public async Task UpdateAsync(OrganisationUpdateRequest request, CancellationToken cancellationToken = default) {
            Organisation existing = await GetByIdAsync(request.Id, cancellationToken: cancellationToken).ConfigureAwait(false) ?? throw new EntityNotFoundExeption(nameof(Organisation), request.Id);

            existing.Name = request.Name;
            existing.Image = request.Image;
        }
        public async Task DeleteAsync(Int64 id, CancellationToken cancellationToken = default) {
            Organisation existing = await GetByIdAsync(id, cancellationToken: cancellationToken).ConfigureAwait(false) ?? throw new EntityNotFoundExeption(nameof(Organisation), id);
            _context.Organisations.Remove(existing);
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default) => _context.SaveChangesAsync(cancellationToken: cancellationToken);

    }
}
