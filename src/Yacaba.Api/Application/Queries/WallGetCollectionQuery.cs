using MediatR;
using Yacaba.Domain.Models;
using Yacaba.Domain.Stores;

namespace Yacaba.Api.Application.Queries {

    public record WallGetCollectionQuery() : IRequest<IQueryable<Wall>> { }

    public class WallGetCollectionHandler : IRequestHandler<WallGetCollectionQuery, IQueryable<Wall>> {

        private readonly IWallStore _store;

        public WallGetCollectionHandler(
            IWallStore store
        ) {
            _store = store;
        }

        public Task<IQueryable<Wall>> Handle(WallGetCollectionQuery request, CancellationToken cancellationToken) {
            return Task.FromResult(_store.GetAll());
        }
    }

}
