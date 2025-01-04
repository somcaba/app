using FluentValidation;
using MediatR;
using Yacaba.Api.Application.Events;
using Yacaba.Domain.Models;
using Yacaba.Domain.Requests;
using Yacaba.Domain.Stores;

namespace Yacaba.Api.Application.Commands {

    public record GymCreateCommand(
        GymCreateRequest Request
    ) : IRequest<Gym> { }

    public class GymCreateCommandHandler : IRequestHandler<GymCreateCommand, Gym> {

        private readonly IGymStore _store;
        private readonly GymCreateCommandValidator _validator;
        private readonly IPublisher _publisher;

        public GymCreateCommandHandler(
            IGymStore store,
            GymCreateCommandValidator validator,
            IPublisher publisher
        ) {
            _store = store;
            _validator = validator;
            _publisher = publisher;
        }

        public async Task<Gym> Handle(GymCreateCommand request, CancellationToken cancellationToken) {
            await _validator.ValidateAndThrowAsync(request.Request, cancellationToken: cancellationToken).ConfigureAwait(false);

            Gym organisation = await _store.CreateAsync(request.Request, cancellationToken: cancellationToken).ConfigureAwait(false);
            await _store.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

            //await _publisher.Publish(new GymCreatedEvent(organisation), cancellationToken: cancellationToken).ConfigureAwait(false);

            return organisation;
        }
    }

    public class GymCreateCommandValidator : AbstractValidator<GymCreateRequest> {
        public GymCreateCommandValidator() {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MinimumLength(1);
            RuleFor(p => p.Name).MaximumLength(200);
        }
    }

}
