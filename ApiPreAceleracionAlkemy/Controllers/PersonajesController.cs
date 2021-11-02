using ApiPreAceleracionAlkemy.Data;
using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.Repositories;
using ApiPreAceleracionAlkemy.ViewModel;
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
    
    public class PersonajesController : ControllerBase
    {
        private readonly IPersonajeRepository _personajeRepository;
        public PersonajesController(IPersonajeRepository personajeRepository)
        {
            _personajeRepository = personajeRepository;
        }

        [HttpGet]
        // TODO : QUERY SOBRE PELICULAS //
        public IActionResult Get(string nombre, short edad)
        {
            var personaje = _personajeRepository.GetPersonajes();

            if (!string.IsNullOrEmpty(nombre))
            {
                personaje = personaje.Where(n => n.Nombre == nombre).ToList();
            }
            if(edad != 0)
            {
                personaje = personaje.Where(e => e.Edad == edad).ToList();
            }
            
            //if (!string.IsNullOrEmpty(pelicula))
            //{
                
            //}

            if (!personaje.Any()) return NoContent();
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
            _personajeRepository.AddEntity(personaje);
            return Ok(personaje);
        }

        [HttpPut]
        [Route("Editar Personaje")]
        //TODO : Agregar al Bindeo la relacion Personaje-Pelicula //
        public  IActionResult Put(PesonajePutViewModel personajeViewModel)
        {
            
            var personajeEdit = new Personaje
            {
                Id = personajeViewModel.Id,   
                Imagen = personajeViewModel.Imagen,
                Nombre = personajeViewModel.Nombre,
                Edad = personajeViewModel.Edad,
                Peso = personajeViewModel.Peso,
                Historia = personajeViewModel.Historia
            };

            if (personajeEdit == null)
            {
                return NotFound($"El personaje con id {personajeEdit.Id} no exite.");
            }

            personajeEdit = _personajeRepository.GetPersonaje(personajeEdit.Id);

            _personajeRepository.UpdateEntity(personajeEdit);
            
            return Ok(personajeEdit);
        }

        [HttpDelete]
        [Route(template:"Eliminar Personaje/{id}")]
        public IActionResult Delete(int id)
        {

            var internalpersonaje = _personajeRepository.GetPersonaje(id);
            
            if(internalpersonaje == null)
            {
                return NotFound($"El personaje con id {id} no exite.");
            }
            _personajeRepository.DeleteEntity(id);
            return Ok($"El personaje {internalpersonaje.Nombre} fue eliminado correctamente.");
        }
    }
}
