using FluentValidation;
using MediatR;
using Yacaba.Domain.Requests;
using Yacaba.Domain.Stores;

namespace Yacaba.Api.Application.Commands {

    public record WallUpdateCommand(
        Int64 WallId,
        WallUpdateRequest Request
    ) : IRequest<Unit> { }

    public class WallUpdateCommandHandler : IRequestHandler<WallUpdateCommand, Unit> {

        private readonly IWallStore _store;
        private readonly WallUpdateCommandValidator _validator;

        public WallUpdateCommandHandler(
            IWallStore store,
            WallUpdateCommandValidator validator
        ) {
            _store = store;
            _validator = validator;
        }

        public async Task<Unit> Handle(WallUpdateCommand request, CancellationToken cancellationToken) {
            if (request.WallId != request.Request.Id) { throw new InvalidDataException("Invalid request"); }

            await _validator.ValidateAndThrowAsync(request.Request, cancellationToken: cancellationToken).ConfigureAwait(false);

            await _store.UpdateAsync(request.Request, cancellationToken: cancellationToken).ConfigureAwait(false);
            await _store.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }

    public class WallUpdateCommandValidator : AbstractValidator<WallUpdateRequest> {
        public WallUpdateCommandValidator() {
            RuleFor(p => p.Id).NotEmpty();
            Include(new WallCreateCommandValidator());
        }
    }

}
