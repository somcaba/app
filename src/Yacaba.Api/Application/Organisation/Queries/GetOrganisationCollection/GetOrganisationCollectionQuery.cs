using MediatR;
using OrganisationEntity = Yacaba.Domain.Models.Organisation;

namespace Yacaba.Api.Application.Organisation.Queries.GetOrganisationCollection {
    public record GetOrganisionCollectionQuery() : IStreamRequest<OrganisationEntity> { }
}
