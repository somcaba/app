using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Yacaba.Domain.Requests;
using Yacaba.Domain.Stores;
using Yacaba.EntityFramework;

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

        private readonly ApplicationDbContext _context;

        public GymUpdateCommandValidator(ApplicationDbContext context) {
            _context = context;

            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Id).MustAsync(IsExistingGymAsync);
            Include(new GymCreateCommandValidator(context));
        }

        private async Task<Boolean> IsExistingGymAsync(Int64 gymId, CancellationToken cancellationToken) {
            return await _context.Gyms.AnyAsync(p => p.Id == gymId, cancellationToken: cancellationToken).ConfigureAwait(false) == true;
        }

    }

}
