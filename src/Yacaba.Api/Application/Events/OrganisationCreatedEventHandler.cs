using MediatR;
using Yacaba.Domain.Models;

namespace Yacaba.Api.Application.Events {

    public record OrganisationCreatedEvent(
        Organisation Organisation
    ) : INotification { }

    public class OrganisationCreatedEventHandler : INotificationHandler<OrganisationCreatedEvent> {
        public Task Handle(OrganisationCreatedEvent request, CancellationToken cancellationToken) {
            Console.WriteLine($"Organisation Created: {request.Organisation.Id}");
            return Task.CompletedTask;
        }
    }
}
