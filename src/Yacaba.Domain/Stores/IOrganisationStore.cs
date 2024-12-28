using Yacaba.Domain.Models;
using Yacaba.Domain.Requests.Organisation;

namespace Yacaba.Domain.Stores {
    public interface IOrganisationStore {

        Task<Organisation?> GetByIdAsync(OrganisationId id, CancellationToken cancellationToken = default);
        IQueryable<Organisation> GetAll();
        Task<Organisation> CreateAsync(CreateOrganisationRequest request, CancellationToken cancellationToken = default);
        Task UpdateAsync(UpdateOrganisationRequest request, CancellationToken cancellationToken = default);
        Task DeleteAsync(OrganisationId id, CancellationToken cancellationToken = default);
        
        Task SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
