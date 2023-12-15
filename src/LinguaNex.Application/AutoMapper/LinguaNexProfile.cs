using AutoMapper;
using LinguaNex.Cultures.Dtos;
using LinguaNex.Entities;
using LinguaNex.Project.Dtos;

namespace LinguaNex.AutoMapper
{
    public class LinguaNexProfile : Profile
    {
        public LinguaNexProfile()
        {
            CreateMap<CreateProjectDto, Projects>(MemberList.Source);
            CreateMap<Projects, ProjectDto>();
            CreateMap<CreateCultureDto, Culture>(MemberList.Source);
            CreateMap<Culture, CultureDto>();
        }
    }
}
