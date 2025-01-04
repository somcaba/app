using MediatR;
using Yacaba.Domain.Models;
using Yacaba.Domain.Stores;

namespace Yacaba.Api.Application.Queries {

    public record GymGetByIdQuery(
        Int64 GymId
    ) : IRequest<Gym?> { }

    public class GymGetByIdHandler : IRequestHandler<GymGetByIdQuery, Gym?> {

        private readonly IGymStore _store;

        public GymGetByIdHandler(
            IGymStore store
        ) {
            _store = store;
        }

        public async Task<Gym?> Handle(GymGetByIdQuery request, CancellationToken cancellationToken) {
            return await _store.GetByIdAsync(request.GymId, cancellationToken).ConfigureAwait(false);
        }
    }

}
