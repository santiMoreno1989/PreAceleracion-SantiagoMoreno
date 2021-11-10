using ApiPreAceleracionAlkemy.Data;
using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.Repositories;
using ApiPreAceleracionAlkemy.ViewModel.GeneroView;
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
    public class GenerosController : ControllerBase
    {

        private readonly IGeneroRepository _generoRepository;
        public GenerosController(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }
        [HttpGet]
        [AllowAnonymous]
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

            if (editarGenero == null)
            {
                return NotFound("El genero  no existe");
            }

            editarGenero.Nombre = generoPutViewModel.Nombre;
            editarGenero.Imagen = generoPutViewModel.Imagen;

            try
            {
                _generoRepository.UpdateEntity(editarGenero);
            }
            catch (Exception)
            {

                return BadRequest();
            }
            
            return Ok(editarGenero);
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
           var generoDelete = _generoRepository.GetGenero(id);
            if(generoDelete == null)
            {
                return NotFound($"el genero con ID {id} no existe");
            }
            try
            {
                _generoRepository.DeleteEntity(id);
            }
            catch (Exception)
            {

               return BadRequest();
            }
            
            return Ok($"El genero {generoDelete.Nombre} ha sido eliminado correctamente.");
        }

    }
}
