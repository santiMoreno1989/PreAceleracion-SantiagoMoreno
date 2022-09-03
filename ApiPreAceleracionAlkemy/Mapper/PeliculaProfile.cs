using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.ViewModel;
using ApiPreAceleracionAlkemy.ViewModel.PeliculaView;
using AutoMapper;

namespace ApiPreAceleracionAlkemy.Mapper
{
    public class PeliculaProfile : Profile
    {
        public PeliculaProfile()
        {
            CreateMap<Pelicula,PeliculasGetViewModel>();
            CreateMap<PeliculaPostViewModel, Pelicula>();
            CreateMap<PeliculaPutViewModel, Pelicula>()
                .ForMember(x=> x.Titulo,map=> map.MapFrom(src=> src.Titulo))
                .ForMember(x=> x.Imagen,map=> map.MapFrom(src=> src.Imagen))
                .ForMember(x=> x.Calificacion,map=> map.MapFrom(src=> src.Calificacion));
        }
    }
}
