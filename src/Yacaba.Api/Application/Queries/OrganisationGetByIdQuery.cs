using MediatR;
using Yacaba.Domain.Models;
using Yacaba.Domain.Stores;

namespace Yacaba.Api.Application.Queries {

    public record OrganisationGetByIdQuery(
        Int64 OrganisationId
    ) : IRequest<Organisation?> { }

    public class OrganisationGetByIdHandler : IRequestHandler<OrganisationGetByIdQuery, Organisation?> {

        private readonly IOrganisationStore _store;

        public OrganisationGetByIdHandler(
            IOrganisationStore store
        ) {
            _store = store;
        }

        public async Task<Organisation?> Handle(OrganisationGetByIdQuery request, CancellationToken cancellationToken) {
            return await _store.GetByIdAsync(request.OrganisationId, cancellationToken).ConfigureAwait(false);
        }
    }

}
