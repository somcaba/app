using FluentValidation;
using MediatR;
using Yacaba.Domain.Requests;
using Yacaba.Domain.Stores;

namespace Yacaba.Api.Application.Commands {

    public record GymUpdateCommand(
        Int64 GymId,
        GymUpdateRequest Request
    ) : IRequest<Unit> { }

    public class GymUpdateCommandHandler : IRequestHandler<GymUpdateCommand, Unit> {

        private readonly IGymStore _store;
        private readonly GymUpdateCommandValidator _validator;

        public GymUpdateCommandHandler(
            IGymStore store,
            GymUpdateCommandValidator validator
        ) {
            _store = store;
            _validator = validator;
        }

        public async Task<Unit> Handle(GymUpdateCommand request, CancellationToken cancellationToken) {
            if (request.GymId != request.Request.Id) { throw new InvalidDataException("Invalid request"); }

            await _validator.ValidateAndThrowAsync(request.Request, cancellationToken: cancellationToken).ConfigureAwait(false);

            await _store.UpdateAsync(request.Request, cancellationToken: cancellationToken).ConfigureAwait(false);
            await _store.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }

    public class GymUpdateCommandValidator : AbstractValidator<GymUpdateRequest> {
        public GymUpdateCommandValidator() {
            RuleFor(p => p.Id).NotEmpty();
            Include(new GymCreateCommandValidator());
        }
    }

}
