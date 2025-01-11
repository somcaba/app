using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Yacaba.Api.Validators;
using Yacaba.Domain.Models;
using Yacaba.Domain.Requests;
using Yacaba.Domain.Stores;
using Yacaba.EntityFramework;

namespace Yacaba.Api.Application.Commands {

    public record GymCreateCommand(
        GymCreateRequest Request
    ) : IRequest<Gym> { }

    public class GymCreateCommandHandler : IRequestHandler<GymCreateCommand, Gym> {

        private readonly IGymStore _store;
        private readonly GymCreateCommandValidator _validator;
        //private readonly IPublisher _publisher;

        public GymCreateCommandHandler(
            IGymStore store,
            GymCreateCommandValidator validator
        //IPublisher publisher
        ) {
            _store = store;
            _validator = validator;
            //_publisher = publisher;
        }

        public async Task<Gym> Handle(GymCreateCommand request, CancellationToken cancellationToken) {
            await _validator.ValidateAndThrowAsync(request.Request, cancellationToken: cancellationToken).ConfigureAwait(false);

            Gym gym = await _store.CreateAsync(request.Request, cancellationToken: cancellationToken).ConfigureAwait(false);
            await _store.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

            //await _publisher.Publish(new GymCreatedEvent(gym), cancellationToken: cancellationToken).ConfigureAwait(false);

            return gym;
        }
    }

    public class GymCreateCommandValidator : AbstractValidator<GymCreateRequest> {

        private readonly ApplicationDbContext _context;

        public GymCreateCommandValidator(ApplicationDbContext context) {
            _context = context;

            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Name).MinimumLength(1);
            RuleFor(p => p.Name).MaximumLength(200);
            RuleFor(p => p.Name).MustAsync(IsUniqueNameAsync).WithMessage("'{PropertyName}' must be unique");

            RuleFor(p => p.Address!).SetValidator(new AdressValidator()).When(p => p.Address is not null);
            RuleFor(p => p.Location!).SetValidator(new LocationValidator()).When(p => p.Location is not null);

            RuleFor(p => p.Organisation).MustAsync(IsExistingOrganisationAsync).When(p => p.Organisation is not null).WithMessage("'{PropertyName}' must be an existing organisation");
        }

        private async Task<Boolean> IsUniqueNameAsync(String name, CancellationToken cancellationToken) {
            return await _context.Gyms.AnyAsync(p => p.Name == name, cancellationToken) == false;
        }

        private async Task<Boolean> IsExistingOrganisationAsync(Int64? organisationId, CancellationToken cancellationToken) {
            ArgumentNullException.ThrowIfNull(organisationId, nameof(organisationId));
            return await _context.Organisations.AnyAsync(p => p.Id == organisationId, cancellationToken: cancellationToken).ConfigureAwait(false) == true;
        }

    }

}
