using MediatR;
using Yacaba.Domain.Models;
using Yacaba.Domain.Stores;

namespace Yacaba.Api.Application.Queries {

    public record OrganisionGetByIdQuery(
        Int64 OrganisationId
    ) : IRequest<Organisation?> { }

    public class OrganisionGetByIdHandler : IRequestHandler<OrganisionGetByIdQuery, Organisation?> {

        private readonly IOrganisationStore _store;

        public OrganisionGetByIdHandler(
            IOrganisationStore store
        ) {
            _store = store;
        }

        public async Task<Organisation?> Handle(OrganisionGetByIdQuery request, CancellationToken cancellationToken) {
            return await _store.GetByIdAsync(request.OrganisationId, cancellationToken).ConfigureAwait(false);
        }
    }

}
