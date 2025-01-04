using FluentValidation;
using MediatR;
using Yacaba.Domain.Stores;

namespace Yacaba.Api.Application.Commands {

    public record GymDeleteCommand(
        Int64 GymId
    ) : IRequest<Unit> { }

    public class GymDeleteCommandHandler : IRequestHandler<GymDeleteCommand, Unit> {

        private readonly IGymStore _store;
        private readonly GymDeleteCommandValidator _validator;

        public GymDeleteCommandHandler(
            IGymStore store,
            GymDeleteCommandValidator validator
        ) {
            _store = store;
            _validator = validator;
        }

        public async Task<Unit> Handle(GymDeleteCommand request, CancellationToken cancellationToken) {
            await _validator.ValidateAndThrowAsync(request, cancellationToken: cancellationToken).ConfigureAwait(false);

            await _store.DeleteAsync(request.GymId, cancellationToken: cancellationToken).ConfigureAwait(false);
            await _store.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }

    public class GymDeleteCommandValidator : AbstractValidator<GymDeleteCommand> {
        public GymDeleteCommandValidator() {
            RuleFor(p => p.GymId).NotEmpty();
        }
    }

}
