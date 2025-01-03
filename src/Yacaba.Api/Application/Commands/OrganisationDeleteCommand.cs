using FluentValidation;
using MediatR;
using Yacaba.Domain.Stores;

namespace Yacaba.Api.Application.Commands {

    public record OrganisationDeleteCommand(
        Int64 OrganisationId
    ) : IRequest<Unit> { }

    public class OrganisationDeleteCommandHandler : IRequestHandler<OrganisationDeleteCommand, Unit> {

        private readonly IOrganisationStore _store;
        private readonly OrganisationDeleteCommandValidator _validator;

        public OrganisationDeleteCommandHandler(
            IOrganisationStore store,
            OrganisationDeleteCommandValidator validator
        ) {
            _store = store;
            _validator = validator;
        }

        public async Task<Unit> Handle(OrganisationDeleteCommand request, CancellationToken cancellationToken) {
            await _validator.ValidateAndThrowAsync(request, cancellationToken: cancellationToken).ConfigureAwait(false);

            await _store.DeleteAsync(request.OrganisationId, cancellationToken: cancellationToken).ConfigureAwait(false);
            await _store.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }

    public class OrganisationDeleteCommandValidator : AbstractValidator<OrganisationDeleteCommand> {
        public OrganisationDeleteCommandValidator() {
            RuleFor(p => p.OrganisationId).NotEmpty();
        }
    }

}
