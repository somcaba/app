using Yacaba.Domain.Models;
using Yacaba.Domain.Requests;

namespace Yacaba.Domain.Stores {
    public interface IOrganisationStore {

        Task<Organisation?> GetByIdAsync(Int64 id, CancellationToken cancellationToken = default);
        IQueryable<Organisation> GetAll();
        Task<Organisation> CreateAsync(OrganisationCreateRequest request, CancellationToken cancellationToken = default);
        Task UpdateAsync(OrganisationUpdateRequest request, CancellationToken cancellationToken = default);
        Task DeleteAsync(Int64 id, CancellationToken cancellationToken = default);
        
        Task SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
