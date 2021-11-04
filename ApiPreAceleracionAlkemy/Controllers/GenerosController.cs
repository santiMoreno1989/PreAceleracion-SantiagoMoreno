using ApiPreAceleracionAlkemy.Data;
using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.Repositories;
using ApiPreAceleracionAlkemy.ViewModel.GeneroView;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Controllers
{
    [ApiController]
    [Route(template:"api/[controller]")]
    public class GenerosController : ControllerBase
    {

        private readonly IGeneroRepository _generoRepository;
        public GenerosController(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var genero = _generoRepository.GetGeneros();

            return Ok(genero);
        }

        [HttpPost]
        public IActionResult Post(GeneroPostViewModel generoPostViewModel)
        {
            var genero = new Genero
            {
                Nombre = generoPostViewModel.Nombre,
                Imagen = generoPostViewModel.Imagen
            };
            _generoRepository.AddEntity(genero);
            return Ok(genero);
        }
        [HttpPut]
        public IActionResult Put(GeneroPutViewModel generoPutViewModel)
        {
            var editarGenero = _generoRepository.GetGenero(generoPutViewModel.Id);
            editarGenero.Nombre = generoPutViewModel.Nombre;
            editarGenero.Imagen = generoPutViewModel.Imagen;
            if(editarGenero == null)
            {
                return BadRequest("El genero no existe");
            }
            _generoRepository.UpdateEntity(editarGenero);
            
            return Ok(editarGenero);
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
           var generoDelete = _generoRepository.GetGenero(id);
            if(generoDelete == null)
            {
                return NotFound($"el genero no existe");
            }
            _generoRepository.DeleteEntity(id);
            return Ok($"El genero {generoDelete.Nombre} ha sido eliminado correctamente.");
        }

    }
}
