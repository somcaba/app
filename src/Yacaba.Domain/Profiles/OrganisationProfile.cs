using AutoMapper;
using Yacaba.Domain.Dtos;
using Yacaba.Domain.Models;

namespace Yacaba.Domain.Profiles {
    public class OrganisationProfile : Profile {
        public OrganisationProfile() {

            //CreateMap<OrganisationId, Int64>().ReverseMap();

            CreateMap<Organisation, OrganisationDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.IsOffical, opt => opt.MapFrom(src => src.IsOffical));
                //.ForAllMembers(p => p.ExplicitExpansion());
        }
    }
}
