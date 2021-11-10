using ApiPreAceleracionAlkemy.Data;
using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.Repositories;
using ApiPreAceleracionAlkemy.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles ="Admin,User")]
    
    public class PersonajesController : ControllerBase
    {
        private readonly IPersonajeRepository _personajeRepository;
        public PersonajesController(IPersonajeRepository personajeRepository)
        {
            _personajeRepository = personajeRepository;
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("characters")]
        public  IActionResult Get(string name, short age, int movies)
        {
            var personaje = _personajeRepository.GetPersonajes();

            if (!string.IsNullOrEmpty(name))
            {
                personaje = personaje.Where(n => n.Nombre == name).ToList();
            }
            if (age != 0)
            {
                personaje = personaje.Where(e => e.Edad == age).ToList();
            }
            if (movies != 0)
            {
                personaje = personaje.Where(p => p.Peliculas.FirstOrDefault(x => x.Id == movies) != null).ToList();
            }

           
;
            return Ok(personaje);
        }

        [HttpPost]
        public IActionResult Post(PersonajePostViewModel personajeViewModel)
        {
            var personaje = new Personaje
            {
                Imagen = personajeViewModel.Imagen,
                Nombre = personajeViewModel.Nombre,
                Edad = personajeViewModel.Edad,
                Peso = personajeViewModel.Peso,
                Historia = personajeViewModel.Historia
            };
            try
            {
               _personajeRepository.AddEntity(personaje);
            }
            catch (Exception)
            {

                return BadRequest();
               
            }
            
            return Ok(personaje);
        }

        [HttpPut]
        public  IActionResult Put(PesonajePutViewModel personajeViewModel)
        {
            var personajeEdit = _personajeRepository.GetPersonaje(personajeViewModel.Id);
            
            if (personajeEdit == null)
            {
                return NotFound($"El personaje solicitado no exite.");
            }
            personajeEdit.Imagen = personajeViewModel.Imagen;
            personajeEdit.Nombre = personajeViewModel.Nombre;
            personajeEdit.Edad = personajeViewModel.Edad;
            personajeEdit.Peso = personajeViewModel.Peso;
            personajeEdit.Historia = personajeViewModel.Historia;

            try
            {
                _personajeRepository.UpdateEntity(personajeEdit);
            }
            catch (Exception)
            {

                return BadRequest();
            }
            
            
            return Ok(personajeEdit);
        }

        [HttpDelete]
        [Route(template:"{id}")]
        public IActionResult Delete(int id)
        {

            var internalpersonaje = _personajeRepository.GetPersonaje(id);
            
            if(internalpersonaje == null)
            {
                return NotFound($"El personaje con id {id} no exite.");
            }
            try
            {
                _personajeRepository.DeleteEntity(id);
            }
            catch (Exception)
            {

                return BadRequest();
            }
            
            return Ok($"El personaje {internalpersonaje.Nombre} fue eliminado correctamente.");
        }
    }
}
