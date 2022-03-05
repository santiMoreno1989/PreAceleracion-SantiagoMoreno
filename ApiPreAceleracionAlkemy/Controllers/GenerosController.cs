using Amazon.Runtime.Internal;
using ApiPreAceleracionAlkemy.Data;
using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.Interfaces;
using ApiPreAceleracionAlkemy.Repositories;
using ApiPreAceleracionAlkemy.ViewModel.GeneroView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Controllers
{
    /// <summary>
    /// Servicios para Listar,Guardar,Editar y Eliminar 
    /// los generos de las peliculas.
    /// </summary>
    [ApiController]
    [Route(template:"api/[controller]")]
    [Produces("application/json")]
    //[Authorize(Roles = "Admin,User")]
    public class GenerosController : ControllerBase
    {

       
        private readonly IGeneroService _generoService;

        public GenerosController(IGeneroService generoService)
        {
            _generoService = generoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> GetGeneros() {

            var generos =await _generoService.GetAll();

            return Ok(generos);
        }


        /// <summary>
        /// Obtiene un Genero de acuerdo a su Id
        /// </summary>
        /// <param name="id">Id del Genero</param>
        /// <returns>Los datos del Genero</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200,Type =typeof(Genero))]
        [ProducesResponseType(404,Type =typeof(Genero))]

        // ** TODO : APLICAR AUTOMAPPER **//

        public async Task<IActionResult> Get(int id)
        {

            var genero = await _generoService.GetById(id);
            if (genero == null)
            {
                return NotFound(new
                {
                    Status = "Error",
                    Messege = "No se encontro ningun genero"
                });
            } 
           
            return Ok(genero);
        }


        /// <summary>
        /// Permite registrar un nuevo genero de pelicula
        /// </summary>
        /// <param name="generoPostViewModel"></param>
        /// <remarks>
        ///   **Sample request** :
        /// 
        /// 
        ///         POST
        ///         {
        ///             "nombre": "Nuevo",
        ///             "imagen": "NotImage"
        ///          
        ///         }
        /// 
        /// </remarks>

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(GeneroPostViewModel))]
        [ProducesResponseType(400, Type = typeof(GeneroPostViewModel))]

        // ** TODO : APLICAR AUTOMAPPER **//

        public async Task<IActionResult> Post(GeneroPostViewModel generoPostViewModel)
        {
            var genero = new Genero
            {
                Nombre = generoPostViewModel.Nombre,
                Imagen = generoPostViewModel.Imagen
            };

            try
            {
                  await  _generoService.Create(genero);
                
            }
            catch (Exception)
            {

                return BadRequest();
            }
            
            
            return Ok(genero);
        }

        /// <summary>
        /// Permite editar un Genero
        /// </summary>
        /// <param name="generoPutViewModel"></param>
        /// <remarks>
        ///   **Sample request** :
        /// 
        /// 
        ///         PUT
        ///         {
        ///             "nombre": "Nuevo",
        ///             "imagen": "NotImage"
        ///         }
        /// 
        /// </remarks>
        /// <response code="200">Se edito el genero correctamente.</response>
        /// <response code="400">No se pudo editar el genero.</response>
        /// <response code="404">No se encontro el genero.</response>


        [HttpPut]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        // ** TODO : APLICAR AUTOMAPPER **//

        public async Task<IActionResult> Put(GeneroPutViewModel generoPutViewModel)
        {
            var editarGenero = await _generoService.GetById(generoPutViewModel.Id);

            if (editarGenero == null)
            {
                return NotFound();
            }



            try
            {
                editarGenero.Nombre = generoPutViewModel.Nombre;
                editarGenero.Imagen = generoPutViewModel.Imagen;

                if (ModelState.IsValid)
                {
                  await  _generoService.Edit(editarGenero);
                }

            }
            catch (Exception)
            {
                if (editarGenero == null)
                {
                    return BadRequest();
                }
            }
            
            return Ok(editarGenero);
        }
        /// <summary>
        /// Permite eliminar un genero
        /// </summary>
        /// <param name="id">ID del genero a eliminar</param>
        /// <remarks>
        ///   **Sample request** :
        /// 
        /// 
        ///         DELETE
        ///         {
        ///             "id": "1"
        ///         }
        /// 
        /// </remarks>
        /// <returns>Datos del genero eliminado</returns>
        /// <response code="200">El genero se elimino correctamente.</response>
        /// <reponse code="404">El genero que desea eliminar no existe.</reponse>
        /// <response code="400">No se pudo eliminar el genero.</response>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
           var generoDelete = _generoService.GetById(id);
            if(generoDelete == null)
            {
                return NotFound($"el genero con ID {id} no existe");
            }
            try
            {
                _generoService.Delete(id);
            }
            catch (Exception)
            {

               return BadRequest("El genero que intenta borrar ya ha sido eliminado");
            }
            
            return Ok();
        }

    }
}
