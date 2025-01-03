using FluentValidation;
using MediatR;
using Yacaba.Api.Application.Events;
using Yacaba.Domain.Models;
using Yacaba.Domain.Requests;
using Yacaba.Domain.Stores;

namespace Yacaba.Api.Application.Commands {

    public record OrganisationCreateCommand(
        OrganisationCreateRequest Request
    ) : IRequest<Organisation> { }

    public class OrganisationCreateCommandHandler : IRequestHandler<OrganisationCreateCommand, Organisation> {

        private readonly IOrganisationStore _store;
        private readonly OrganisationCreateCommandValidator _validator;
        private readonly IPublisher _publisher;

        public OrganisationCreateCommandHandler(
            IOrganisationStore store,
            OrganisationCreateCommandValidator validator,
            IPublisher publisher
        ) {
            _store = store;
            _validator = validator;
            _publisher = publisher;
        }

        public async Task<Organisation> Handle(OrganisationCreateCommand request, CancellationToken cancellationToken) {
            await _validator.ValidateAndThrowAsync(request.Request, cancellationToken: cancellationToken).ConfigureAwait(false);

            Organisation organisation = await _store.CreateAsync(request.Request, cancellationToken: cancellationToken).ConfigureAwait(false);
            await _store.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

            await _publisher.Publish(new OrganisationCreatedEvent(organisation), cancellationToken: cancellationToken).ConfigureAwait(false);

            return organisation;
        }
    }

    public class OrganisationCreateCommandValidator : AbstractValidator<OrganisationCreateRequest> {
        public OrganisationCreateCommandValidator() {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MinimumLength(1);
            RuleFor(p => p.Name).MaximumLength(200);
        }
    }

}
