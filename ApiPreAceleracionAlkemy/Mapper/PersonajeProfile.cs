using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.ViewModel;
using ApiPreAceleracionAlkemy.ViewModel.PersonajeView;
using AutoMapper;

namespace ApiPreAceleracionAlkemy.Mapper
{
    public class PersonajeProfile : Profile
    {
        public PersonajeProfile()
        {
            CreateMap<PersonajePostViewModel, Personaje>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre));
            
            CreateMap<PesonajePutViewModel, Personaje>();

            CreateMap<Personaje, PersonajeGetViewModel>();
        }
    }
}
