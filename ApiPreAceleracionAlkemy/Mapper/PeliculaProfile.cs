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
            CreateMap<PeliculaPutViewModel, Pelicula>();
        }
    }
}
