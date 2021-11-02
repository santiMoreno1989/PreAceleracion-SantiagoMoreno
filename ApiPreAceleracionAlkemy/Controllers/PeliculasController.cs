using ApiPreAceleracionAlkemy.Data;
using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.Repositories;
using ApiPreAceleracionAlkemy.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Controllers
{
    [ApiController]
    [Route(template:"api/[controller]")]
    public class PeliculasController : ControllerBase
    {
        private readonly IPeliculaRepository _peliculaRepository;
        public PeliculasController(IPeliculaRepository peliculaRepository)
        {
            _peliculaRepository = peliculaRepository;
        }
        [HttpGet]
        public IActionResult Get(string nombre)
        {
            var peliculas = _peliculaRepository.GetPeliculas();
            if (!string.IsNullOrEmpty(nombre))
            {
                peliculas = (from b in peliculas where b.Titulo == nombre
                             orderby b.Titulo ascending
                             select b).ToList();
            }
            return Ok(peliculas);
        }
        [HttpPost]
        public IActionResult Post(PeliculaPostViewModel peliculaViewModel)
        {
            var pelicula = new Pelicula
            {
                Imagen = peliculaViewModel.Imagen,
                Titulo = peliculaViewModel.Titulo,
                FechaCreacion = peliculaViewModel.FechaCreacion,
                Calificacion = peliculaViewModel.Calificacion
            };
            _peliculaRepository.AddEntity(pelicula);
            
            return Ok(pelicula);
        }
        [HttpPut]
        //TODO Agregar al bindeo Relacion Pelicula- Personaje y Pelicula-genero //
        public IActionResult Put(PeliculaPutViewModel peliculaViewModel)
        {

            var movie = new Pelicula
            {
                Id = peliculaViewModel.Id,
                Imagen = peliculaViewModel.Imagen,
                Titulo = peliculaViewModel.Titulo,
                FechaCreacion = peliculaViewModel.FechaCreacion,
                Calificacion = peliculaViewModel.Calificacion

            };

            movie = _peliculaRepository.GetPelicula(movie.Id);

            if(movie == null)
            {
                return NotFound("Pelicula no encontrada");
            }
            
            _peliculaRepository.AddEntity(movie);

            return Ok(movie);
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var peliculas = _peliculaRepository.GetPelicula(id);
            if (peliculas == null)
            {
                return NotFound("La pelicula solicitada no existe");
            }
            _peliculaRepository.DeleteEntity(id);
            return Ok($"La pelicula {peliculas.Titulo} ha sido borrada correctamente");
        }
    }
}
