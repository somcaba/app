using FluentValidation;
using MediatR;
using Yacaba.Domain.Requests;
using Yacaba.Domain.Stores;

namespace Yacaba.Api.Application.Commands {

    public record OrganisationUpdateCommand(
        Int64 OrganisationId,
        OrganisationUpdateRequest Request
    ) : IRequest<Unit> { }

    public class OrganisationUpdateCommandHandler : IRequestHandler<OrganisationUpdateCommand, Unit> {

        private readonly IOrganisationStore _store;
        private readonly OrganisationUpdateCommandValidator _validator;

        public OrganisationUpdateCommandHandler(
            IOrganisationStore store,
            OrganisationUpdateCommandValidator validator
        ) {
            _store = store;
            _validator = validator;
        }

        public async Task<Unit> Handle(OrganisationUpdateCommand request, CancellationToken cancellationToken) {
            if (request.OrganisationId != request.Request.Id) { throw new InvalidDataException("Invalid request"); }

            await _validator.ValidateAndThrowAsync(request.Request, cancellationToken: cancellationToken).ConfigureAwait(false);

            await _store.UpdateAsync(request.Request, cancellationToken: cancellationToken).ConfigureAwait(false);
            await _store.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

            return Unit.Value;
        }
    }

    public class OrganisationUpdateCommandValidator : AbstractValidator<OrganisationUpdateRequest> {
        public OrganisationUpdateCommandValidator() {
            RuleFor(p => p.Id).NotEmpty();
            Include(new OrganisationCreateCommandValidator());
        }
    }

}
