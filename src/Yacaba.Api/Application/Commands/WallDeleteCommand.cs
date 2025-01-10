using FluentValidation;
using MediatR;
using Yacaba.Domain.Stores;

namespace Yacaba.Api.Application.Commands {

    public record WallDeleteCommand(
        Int64 WallId
    ) : IRequest<Unit> { }

    public class WallDeleteCommandHandler : IRequestHandler<WallDeleteCommand, Unit> {

        private readonly IWallStore _store;
        private readonly WallDeleteCommandValidator _validator;

        public WallDeleteCommandHandler(
            IWallStore store,
            WallDeleteCommandValidator validator
        ) {
            _store = store;
            _validator = validator;
        }

        public async Task<Unit> Handle(WallDeleteCommand request, CancellationToken cancellationToken) {
            await _validator.ValidateAndThrowAsync(request, cancellationToken: cancellationToken).ConfigureAwait(false);

            await _store.DeleteAsync(request.WallId, cancellationToken: cancellationToken).ConfigureAwait(false);
            await _store.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }

    public class WallDeleteCommandValidator : AbstractValidator<WallDeleteCommand> {
        public WallDeleteCommandValidator() {
            RuleFor(p => p.WallId).NotEmpty();
        }
    }

}
