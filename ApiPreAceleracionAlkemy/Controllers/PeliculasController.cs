using ApiPreAceleracionAlkemy.Data;
using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.Repositories;
using ApiPreAceleracionAlkemy.ViewModel;
using ApiPreAceleracionAlkemy.ViewModel.PeliculaView;
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
        private readonly IUnitOfWork _unitOfWork;
        public PeliculasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetList(int Id, string nombre, int IdGenero)
        {
            List <Pelicula> filtroPelis = _unitOfWork.Pelicula.GetPeliculas();
            var model = new List<PeliculasGetViewModel>();
            foreach (var item in filtroPelis)
            {
                model.Add(new PeliculasGetViewModel
                {
                    Titulo = item.Titulo,
                    Imagen = item.Imagen,
                    FechaCreacion = item.FechaCreacion
            });
            }
            
            return Ok(model);
        }

        [HttpGet]
        [Route("movies")]
        [AllowAnonymous]
        public IActionResult Get(string name,int genre, string order)
        {
            var peliculas = _unitOfWork.Pelicula.GetPeliculas();
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
                _unitOfWork.Pelicula.AddEntity(pelicula);
                _unitOfWork.Complete();
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
            var  movie = _unitOfWork.Pelicula.GetPelicula(peliculaViewModel.Id);

            if(movie == null)
            {
                return NotFound("Pelicula no encontrada");
            }
            movie.Imagen = peliculaViewModel.Imagen;
            movie.Titulo = peliculaViewModel.Titulo;
            movie.Calificacion = peliculaViewModel.Calificacion;

            try
            {
                _unitOfWork.Pelicula.UpdateEntity(movie);
                _unitOfWork.Complete();
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
            var peliculas = _unitOfWork.Pelicula.GetPelicula(id);
            if (peliculas == null)
            {
                return NotFound($"La pelicula con ID {id}no existe");
            }
            try
            {
                _unitOfWork.Pelicula.DeleteEntity(id);
            }
            catch (Exception)
            {

                return BadRequest();
            }
            
            return Ok($"La pelicula {peliculas.Titulo} ha sido borrada correctamente");
        }
    }
}
