using AutoMapper;
using Speaker.Service.Protos;

namespace Speaker.Service.Mappings
{
    public class SpeakerMappingProfile : Profile
    {
        public SpeakerMappingProfile()
        {
            CreateMap<Domain.Speaker, SpeakerResponse>();
            CreateMap<SpeakerUpdateRequest, Domain.Speaker>();
        }
    }
}
