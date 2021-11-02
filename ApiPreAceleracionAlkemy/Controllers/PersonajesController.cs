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
        public IActionResult Get(string nombre)
        {
            var personaje = _personajeRepository.GetPersonajes();

            if (!string.IsNullOrEmpty(nombre))
            {
                personaje = personaje.Where(n => n.Nombre == nombre).ToList();
            }
            
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
        public  IActionResult Put(Personaje personaje)
        {
            var internalPersonaje = _personajeRepository.GetPersonaje(personaje.Id);
            if(internalPersonaje == null)
            {
                return NotFound($"El personaje con id {personaje.Id} no exite.");
            }
            internalPersonaje.Imagen = personaje.Imagen;
            internalPersonaje.Nombre = personaje.Nombre;
            internalPersonaje.Edad = personaje.Edad;
            internalPersonaje.Peso = personaje.Peso;
            internalPersonaje.Historia = personaje.Historia;

            _personajeRepository.UpdateEntity(personaje);
            
            return Ok(personaje);
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
