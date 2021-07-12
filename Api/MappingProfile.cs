using AutoMapper;
using DTO;
using Models;

namespace Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Autor, AutorDTO>();
        }
    }
}
