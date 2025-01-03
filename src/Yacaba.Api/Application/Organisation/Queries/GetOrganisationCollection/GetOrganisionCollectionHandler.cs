using MediatR;
using Microsoft.EntityFrameworkCore;
using Yacaba.Domain.Stores;
using OrganisationEntity = Yacaba.Domain.Models.Organisation;

namespace Yacaba.Api.Application.Organisation.Queries.GetOrganisationCollection {

    public class GetOrganisionCollectionHandler : IStreamRequestHandler<GetOrganisionCollectionQuery, OrganisationEntity> {

        private readonly IOrganisationStore _store;

        public GetOrganisionCollectionHandler(
            IOrganisationStore store
        ) {
            _store = store;
        }

        public IAsyncEnumerable<OrganisationEntity> Handle(GetOrganisionCollectionQuery request, CancellationToken cancellationToken) {
            return _store.GetAll().AsAsyncEnumerable();
        }

    }
}
