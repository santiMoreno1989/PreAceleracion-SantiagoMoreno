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
        [Route("character")]
        public IActionResult Get(string name,int genre)
        {
            var peliculas = _peliculaRepository.GetPeliculas();

            if (!string.IsNullOrEmpty(name))
            {
                peliculas = (from b in peliculas where b.Titulo == name
                             select b).ToList();
            }
            if(genre != 0)
            {
                peliculas = peliculas.Where(x => x.Personajes.FirstOrDefault(m => m.Id == genre) != null).ToList();
            }
            if (!peliculas.Any()) return NoContent();

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
            var  movie = _peliculaRepository.GetPelicula(peliculaViewModel.Id);

            if(movie == null)
            {
                return NotFound("Pelicula no encontrada");
            }
            movie.Imagen = peliculaViewModel.Imagen;
            movie.Titulo = peliculaViewModel.Titulo;
            movie.Calificacion = peliculaViewModel.Calificacion;

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
