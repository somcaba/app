using MediatR;
using Yacaba.Domain.Models;
using Yacaba.Domain.Stores;

namespace Yacaba.Api.Application.Queries {

    public record OrganisationGetCollectionQuery() : IRequest<IQueryable<Organisation>> { }

    public class OrganisationGetCollectionHandler : IRequestHandler<OrganisationGetCollectionQuery, IQueryable<Organisation>> {

        private readonly IOrganisationStore _store;

        public OrganisationGetCollectionHandler(
            IOrganisationStore store
        ) {
            _store = store;
        }

        public Task<IQueryable<Organisation>> Handle(OrganisationGetCollectionQuery request, CancellationToken cancellationToken) {
            return Task.FromResult(_store.GetAll());
        }
    }

}
