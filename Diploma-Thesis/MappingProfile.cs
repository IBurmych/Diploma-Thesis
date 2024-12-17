using AutoMapper;
using Diploma_Thesis.Entities;
using Diploma_Thesis.Models;

namespace Diploma_Thesis
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Client, ClientDetailedModel>().ReverseMap();
            CreateMap<Client, ClientSimpleModel>().ReverseMap();
            CreateMap<Expertise, ExpertiseModel>().ReverseMap();
            CreateMap<Diapason, FullDiapasonModel>().ReverseMap();
        }
    }
}
