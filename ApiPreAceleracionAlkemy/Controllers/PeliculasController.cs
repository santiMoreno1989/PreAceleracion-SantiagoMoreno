using ApiPreAceleracionAlkemy.Data;
using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.Repositories;
using ApiPreAceleracionAlkemy.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Controllers
{
    
    [ApiController]
    [Route(template:"api/[controller]")]
    [Authorize(Roles = "Admin,User")]
    public class PeliculasController : ControllerBase
    {
        private readonly IPeliculaRepository _peliculaRepository;
        public PeliculasController(IPeliculaRepository peliculaRepository)
        {
            _peliculaRepository = peliculaRepository;
        }

        [HttpGet]
        [Route("movies")]
        [AllowAnonymous]
        // TODO : Falta Query Orden ASC & DESC //
        public IActionResult Get(string name,int genre, string order)
        {
            var peliculas = _peliculaRepository.GetPeliculas();
            string ASC = "ASC";
            string DESC = "DESC";
            if (!string.IsNullOrEmpty(name))
                {
                    peliculas = (from b in peliculas where b.Titulo == name
                                select b).ToList();
                }
            
            if(genre != 0)
                {
                    peliculas = peliculas.Where(x => x.Genero.FirstOrDefault(z => z.Id == genre) != null).ToList();
                }
           
             if (order == ASC)
                {
                    peliculas = (from A in peliculas
                                 orderby A.Titulo ascending
                                 select A).ToList();
                }

            if (order == DESC)
                {
                    peliculas = (from D in peliculas orderby D.Titulo descending select D).ToList();
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
            try
            {
                _peliculaRepository.AddEntity(pelicula);
            }
            catch (Exception)
            {

                return BadRequest();
            }

                
        
            return Ok(pelicula);
        }
        [HttpPut]
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

            try
            {
                _peliculaRepository.UpdateEntity(movie);
            }
            catch (Exception)
            {

                return BadRequest();
            }
            

            return Ok(movie);
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var peliculas = _peliculaRepository.GetPelicula(id);
            if (peliculas == null)
            {
                return NotFound($"La pelicula con ID {id}no existe");
            }
            try
            {
                _peliculaRepository.DeleteEntity(id);
            }
            catch (Exception)
            {

                return BadRequest();
            }
            
            return Ok($"La pelicula {peliculas.Titulo} ha sido borrada correctamente");
        }
    }
}
