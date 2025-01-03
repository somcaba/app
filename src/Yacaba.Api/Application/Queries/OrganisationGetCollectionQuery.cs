using MediatR;
using Yacaba.Domain.Models;
using Yacaba.Domain.Stores;

namespace Yacaba.Api.Application.Queries {

    public record OrganisionGetCollectionQuery() : IRequest<IQueryable<Organisation>> { }

    public class OrganisionGetCollectionHandler : IRequestHandler<OrganisionGetCollectionQuery, IQueryable<Organisation>> {

        private readonly IOrganisationStore _store;

        public OrganisionGetCollectionHandler(
            IOrganisationStore store
        ) {
            _store = store;
        }

        public Task<IQueryable<Organisation>> Handle(OrganisionGetCollectionQuery request, CancellationToken cancellationToken) {
            return Task.FromResult(_store.GetAll());
        }
    }

}
