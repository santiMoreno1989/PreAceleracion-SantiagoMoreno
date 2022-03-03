using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.ViewModel;
using ApiPreAceleracionAlkemy.ViewModel.PeliculaView;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
