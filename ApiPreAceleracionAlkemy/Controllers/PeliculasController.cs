using ApiPreAceleracionAlkemy.Data;
using ApiPreAceleracionAlkemy.Entities;
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
        private readonly ApplicationDbContext  _context;
        public PeliculasController(ApplicationDbContext context)
        {
            _context= context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Peliculas.ToList());
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
            _context.Peliculas.Add(pelicula);
            _context.SaveChanges();
            return Ok(pelicula);
        }
        [HttpPut]
        //TODO Agregar al bindeo Relacion Pelicula- Personaje y Pelicula-genero //
        public IActionResult Put(Pelicula pelicula)
        {
            if(_context.Peliculas.FirstOrDefault(m => m.Id == pelicula.Id)== null)
            {
                return BadRequest("La pelicula no se ha encontrado");
            }
            var internalPelicula = _context.Peliculas.Find(pelicula.Id);
            internalPelicula.Titulo = pelicula.Titulo;
            internalPelicula.FechaCreacion = pelicula.FechaCreacion;
            internalPelicula.Calificacion = pelicula.Calificacion;
            _context.SaveChanges();
            return Ok(_context.Peliculas.ToList());
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            if (_context.Peliculas.FirstOrDefault(m => m.Id == id) == null)
            {
                return BadRequest("La pelicula que desea eliminar no se ha encontrado");
            }
            var internalPelicula = _context.Peliculas.Find(id);
            _context.Peliculas.Remove(internalPelicula);
            _context.SaveChanges();

            return Ok(_context.Peliculas.ToList());
        }
    }
}
