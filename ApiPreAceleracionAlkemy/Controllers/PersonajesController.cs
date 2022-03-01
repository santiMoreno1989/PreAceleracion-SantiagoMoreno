using ApiPreAceleracionAlkemy.Data;
using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.Repositories;
using ApiPreAceleracionAlkemy.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Produces("application/json")]
    public class PersonajesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public PersonajesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Lista los Personajes
        /// </summary>
        /// <param name="name"> Nombre del personaje.</param>
        /// <param name="age"> Edad del personaje.</param>
        /// <param name="movies"> Pelicula vinculada.</param>
        /// <response code="200">Se listo con exito los personajes</response>
        /// <response code="204">No existe personajes para listar.</response>

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("characters")]
        public  IActionResult Get(string name, short? age, int? movies)
        {
            var personaje = _unitOfWork.Personaje.GetPersonajes();
            

            if (!string.IsNullOrEmpty(name))
            {
                personaje = personaje.Where(n => n.Nombre  == name).ToList();
            }
            if (age != null)
            {
                personaje = personaje.Where(e => e.Edad == age).ToList();
            }
            if (movies != null)
            {
                personaje = personaje.Where(p => p.Peliculas.FirstOrDefault(x => x.Id == movies) != null).ToList();
            }

            if (!personaje.Any()) {
                return NoContent();
            }

            return Ok(personaje);
        }

        /// <summary>
        ///  Crea un personaje
        /// </summary>
        /// <remarks>
        ///         **Sample Request** :
        ///                     
        ///                 POST
        ///                 {
        ///                    "Imagen":"ImagenPrueba.jpge",
        ///                    "Nombre":"Selena Gomez",
        ///                    "Edad":"46",
        ///                    "Peso":"45",
        ///                    "Historia":"descripcion de su carrera profesional"
        ///                     
        ///                 }
        /// 
        /// </remarks>
        /// <param name="personajeViewModel"></param>
        /// <response code="200">Se creo con exito el Personaje.</response>
        /// <response code="400">No se pudo crear el Personaje</response>

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
                _unitOfWork.Personaje.AddEntity(personaje);
                _unitOfWork.Complete();
            }
            catch (Exception)
            {

                return BadRequest();
               
            }
            
            return Ok(personaje);
        }

        /// <summary>
        /// Edita un Personaje
        /// </summary>
        /// <remarks>
        ///         **Sample Request** :
        ///         
        ///                 PUT
        ///                     {
        ///                         "Imagen":"EditedImage.png",
        ///                         "Nombre":"ExampleName",
        ///                         "Edad":"18",
        ///                         "Peso":"120",
        ///                         "Historia":"descripcion de su carrera profesional editar"
        ///                 }
        /// 
        /// 
        /// </remarks>
        /// <param name="personajeViewModel"></param>
        /// <response code="200">Se edito exitosamente el Personaje.</response>
        /// <response code="400">No se pudo editar el Personaje.</response>
        /// <response code="404">el Personaje que desea editar no existe.</response>

        [HttpPut]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public  IActionResult Put(PesonajePutViewModel personajeViewModel)
        {
            var personajeEdit = _unitOfWork.Personaje.GetPersonaje(personajeViewModel.Id);
            
            if (personajeEdit == null)
            {
                return NotFound();
            }
            personajeEdit.Imagen = personajeViewModel.Imagen;
            personajeEdit.Nombre = personajeViewModel.Nombre;
            personajeEdit.Edad = personajeViewModel.Edad;
            personajeEdit.Peso = personajeViewModel.Peso;
            personajeEdit.Historia = personajeViewModel.Historia;

            try
            {
                _unitOfWork.Personaje.UpdateEntity(personajeEdit);
                _unitOfWork.Complete();
            }
            catch (Exception)
            {

                return BadRequest();
            }
            
            
            return Ok(personajeEdit);
        }

        /// <summary>
        ///  Elimina un Personaje
        /// </summary>
        /// <remarks>
        ///         **Sample Request** :
        ///                 DELETE
        ///                 {
        ///                     "id":"1"
        ///                 }
        /// 
        /// </remarks>
        /// <param name="id"> ID del Personaje</param>
        /// <response code="200">La solicitud ha tenido éxito</response>
        /// <response code="400">El servidor no puede procesar la petición.</response>
        /// <response code="404">No Encontrado.</response>

        [HttpDelete]
        [AllowAnonymous]
        [Route(template:"{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {

            var internalpersonaje = _unitOfWork.Personaje.GetPersonaje(id);
            
            if(internalpersonaje == null)
            {
                return NotFound();
            }
            try
            {
                _unitOfWork.Personaje.DeleteEntity(id);
            }
            catch (Exception)
            {

                return BadRequest();
            }
            
            return Ok();
        }
    }
}
