using FluentValidation;
using MediatR;
using Yacaba.Domain.Models;
using Yacaba.Domain.Requests;
using Yacaba.Domain.Stores;

namespace Yacaba.Api.Application.Commands {

    public record WallCreateCommand(
        WallCreateRequest Request
    ) : IRequest<Wall> { }

    public class WallCreateCommandHandler : IRequestHandler<WallCreateCommand, Wall> {

        private readonly IWallStore _store;
        private readonly WallCreateCommandValidator _validator;
        private readonly IPublisher _publisher;

        public WallCreateCommandHandler(
            IWallStore store,
            WallCreateCommandValidator validator,
            IPublisher publisher
        ) {
            _store = store;
            _validator = validator;
            _publisher = publisher;
        }

        public async Task<Wall> Handle(WallCreateCommand request, CancellationToken cancellationToken) {
            await _validator.ValidateAndThrowAsync(request.Request, cancellationToken: cancellationToken).ConfigureAwait(false);

            Wall wall = await _store.CreateAsync(request.Request, cancellationToken: cancellationToken).ConfigureAwait(false);
            await _store.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

            //await _publisher.Publish(new WallCreatedEvent(wall), cancellationToken: cancellationToken).ConfigureAwait(false);

            return wall;
        }
    }

    public class WallCreateCommandValidator : AbstractValidator<WallCreateRequest> {
        public WallCreateCommandValidator() {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MinimumLength(1);
            RuleFor(p => p.Name).MaximumLength(200);
        }
    }

}
