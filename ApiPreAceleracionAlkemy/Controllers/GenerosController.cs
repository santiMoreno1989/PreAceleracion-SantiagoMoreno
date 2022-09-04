using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.Exceptions;
using ApiPreAceleracionAlkemy.Services;
using ApiPreAceleracionAlkemy.ViewModel;
using ApiPreAceleracionAlkemy.ViewModel.GeneroView;
using ApiPreAceleracionAlkemy.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Retorna un listado de generos IQueryable
        /// </summary>
        /// <param name="pageIndex">1</param>
        /// <param name="pageSize">10</param>
        /// <param name="orden"></param>
        /// <returns></returns>
        [HttpGet("Generos")]
        public async Task<ActionResult<Pagination<GeneroGetViewModel>>> GetGenerosPaginados([FromQuery] int pageIndex, [FromQuery] int pageSize,[FromQuery] string orden) 
            => Ok(await _generoService.GetGenerosAsync(pageIndex,pageSize,orden));

        /// <summary>
        ///  Retorna un listado de generos IEnumerable.
        /// </summary>
        /// <returns></returns>
        [HttpGet("grid")]
        public async Task<ActionResult<IEnumerable<Genero>>> GetGeneros() => Ok(await _generoService.GetAll());

        /// <summary>
        /// Obtiene un Genero de acuerdo a su Id
        /// </summary>
        /// <param name="id">Id del Genero</param>
        /// <returns>Los datos del Genero</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Genero))]
        [ProducesResponseType(404, Type = typeof(Genero))]
        public async Task<IActionResult> Get(int id) => Ok(await _generoService.GetById(id));

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
        public async Task<IActionResult> Post(GeneroPostViewModel generoPostViewModel) => Ok(await _generoService.Add(generoPostViewModel));

        /// <summary>
        /// Permite editar un Genero
        /// </summary>
        /// <param name="id"></param>
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
        [HttpPut("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id,[FromBody] GeneroPutViewModel generoPutViewModel) => Ok(await _generoService.Update(id,generoPutViewModel));

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
        public async Task<IActionResult> Delete(int id) => Ok(await _generoService.Delete(id));

        [HttpGet("GetByCondition")]
        public async Task<IActionResult> GetByName(string nombre) 
        {
           return Ok(await _generoService.GetByCondition(nombre));
        } 
    }
}
