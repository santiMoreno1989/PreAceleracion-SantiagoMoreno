using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.Services;
using ApiPreAceleracionAlkemy.ViewModel;
using ApiPreAceleracionAlkemy.ViewModel.PersonajeView;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles ="Admin,User")]
    [Produces("application/json")]
    public class PersonajesController : ControllerBase
    {
        private readonly IPersonajeService _personajeService;
        private readonly IMapper _mapper;
        public PersonajesController(IMapper mapper, IPersonajeService personajeService)
        {
            //_unitOfWork = unitOfWork;
            _mapper = mapper;
            _personajeService = personajeService;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("PersonajesList")]
        public async  Task<ActionResult<IEnumerable<PersonajeGetViewModel>>> GetList() {

            //var personaje = _unitOfWork.Personaje.GetAllEntities();
            var personajes = await _personajeService.GetAll();
            var personajesVm = _mapper.Map<IEnumerable<PersonajeGetViewModel>>(personajes);

            return Ok(personajesVm);
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
        public async Task<ActionResult<Personaje>> Post(PersonajePostViewModel personajeViewModel)
        {
            var personaje = _mapper.Map<Personaje>(personajeViewModel);

            try
            {
               await _personajeService.Add(personaje);
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
        public async Task  <ActionResult<Personaje>> Put(PesonajePutViewModel personajeViewModel)
        {
            var personajeEdit = await _personajeService.GetById(personajeViewModel.Id);
            
            if (personajeEdit == null)
            {
                return NotFound();
            }

            _mapper.Map(personajeViewModel, personajeEdit);

            try
            {
                await _personajeService.Update(personajeEdit);
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
        public async Task <IActionResult> Delete(int id)
        {
            await  _personajeService.Delete(id);
            
            return Ok("Se elimino el personaje correctamente.");
        }
    }
}
