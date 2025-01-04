using MediatR;
using Yacaba.Domain.Models;
using Yacaba.Domain.Stores;

namespace Yacaba.Api.Application.Queries {

    public record GymGetCollectionQuery() : IRequest<IQueryable<Gym>> { }

    public class GymGetCollectionHandler : IRequestHandler<GymGetCollectionQuery, IQueryable<Gym>> {

        private readonly IGymStore _store;

        public GymGetCollectionHandler(
            IGymStore store
        ) {
            _store = store;
        }

        public Task<IQueryable<Gym>> Handle(GymGetCollectionQuery request, CancellationToken cancellationToken) {
            return Task.FromResult(_store.GetAll());
        }
    }

}
