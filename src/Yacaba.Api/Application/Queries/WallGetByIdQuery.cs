using MediatR;
using Yacaba.Domain.Models;
using Yacaba.Domain.Stores;

namespace Yacaba.Api.Application.Queries {

    public record WallGetByIdQuery(
        Int64 WallId
    ) : IRequest<Wall?> { }

    public class WallGetByIdHandler : IRequestHandler<WallGetByIdQuery, Wall?> {

        private readonly IWallStore _store;

        public WallGetByIdHandler(
            IWallStore store
        ) {
            _store = store;
        }

        public async Task<Wall?> Handle(WallGetByIdQuery request, CancellationToken cancellationToken) {
            return await _store.GetByIdAsync(request.WallId, cancellationToken).ConfigureAwait(false);
        }
    }

}
