using AutoMapper;
using LinguaNex.Cultures.Dtos;
using LinguaNex.Entities;
using LinguaNex.Project.Dtos;
using LinguaNex.Resources.Dtos;

namespace LinguaNex.AutoMapper
{
    public class LinguaNexProfile : Profile
    {
        public LinguaNexProfile()
        {
            CreateMap<CreateProjectDto, Projects>(MemberList.Source);
            CreateMap<Projects, ProjectDto>();
            CreateMap<CreateCultureDto, Culture>(MemberList.None);
            CreateMap<Culture, CultureDto>();
            CreateMap<CreateResourceDto, Resource>(MemberList.None);
            CreateMap<Resource, ResourceDto>();
        }
    }
}
