namespace Templates.Api.Profiles
{
    using AutoMapper;
    using Templates.Api.DTOs;
    using Templates.Api.Entities;

    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserDto, User>()
                .ForMember(user => user.Id, opt => opt.Condition(src => src.Id.HasValue))
                .ReverseMap();

            CreateMap<TemplateDto, Template>()
                .ForMember(template => template.Id, opt => opt.Condition(src => src.Id.HasValue))
                .ReverseMap();
        }
    }
}
