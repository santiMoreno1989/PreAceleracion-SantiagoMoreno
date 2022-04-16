using ApiPreAceleracionAlkemy.Entities;
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> GetGeneros() {

            return Ok(await _generoService.GetAll());
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
                  await  _generoService.Add(genero);
                
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
                  await  _generoService.Update(editarGenero);
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
        public async  Task<IActionResult> Delete(int id)
        {
            await  _generoService.Delete(id);
            
            return Ok("Se elimino correctamente el genero   ");
        }

        [HttpGet("GetByCondition")]
        public IActionResult GetByName(string nombre) => Ok(_generoService.GetByCondition(nombre));
    }
}
