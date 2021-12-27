using Amazon.Runtime.Internal;
using ApiPreAceleracionAlkemy.Data;
using ApiPreAceleracionAlkemy.Entities;
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
    [ApiController]
    [Route(template:"api/[controller]")]
    [Produces("application/json")]
    //[Authorize(Roles = "Admin,User")]
    public class GenerosController : ControllerBase
    {

       
        private readonly IUnitOfWork _unitOfWork;
        public GenerosController(IUnitOfWork unitOfWork)
        {
           
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Retorna un listado de Generos
        /// </summary>
        /// <response code="200">Retorna una lista de Generos</response>
        /// <response code="404">No existen generos</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        
        [Route("Prueba")]
        public   IActionResult TryGet()
        {
            var modelGeneros = _unitOfWork.Genero.GetAllEntities();
            if (modelGeneros == null)
            {
                return NotFound();
            }
            var userVM = new List<GeneroGetViewModel>();
            foreach (var item in modelGeneros)
            {
                userVM.Add(
                    new GeneroGetViewModel
                    {
                        Nombre = item.Nombre,
                        Imagen = item.Imagen,
                        TimeStams = item.TimeStams
                    }
                    );
            }
            return Ok(userVM.OrderBy(x=> x.Nombre).Select(p=> p.Imagen));
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            if (id == 0)
            {
                return NotFound(); 
            }
            var genero = _unitOfWork.Genero.GetGenero(id);
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
        /// Crear un Genero
        /// </summary>
        /// <param name="generoPostViewModel"></param>
        /// <remarks>
        ///   **Sample request** :
        /// 
        /// 
        ///         POST
        ///         {
        ///             "nombre": "Nuevo",
        ///             "imagen": "NotImage",
        ///             "timeStams": "2021-12-20T01:56:35.181"
        ///         }
        /// 
        /// </remarks>
        /// <response code="201">Crea un Genero</response>
        /// <response code="400">No se pudo crear el genero</response>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(200, Type = typeof(Genero))]

        public IActionResult Post(GeneroPostViewModel generoPostViewModel)
        {
            var genero = new Genero
            {
                Nombre = generoPostViewModel.Nombre,
                Imagen = generoPostViewModel.Imagen,
                TimeStams = generoPostViewModel.TimeStams
            };

            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.Genero.AddEntity(genero);
                    _unitOfWork.Complete();
                }
            }
            catch (Exception)
            {

                return BadRequest();
            }
            
            
            return Ok(genero);
        }

        /// <summary>
        /// Edita un Genero
        /// </summary>
        /// <param name="generoPutViewModel"></param>
        /// <remarks>
        ///   **Sample request** :
        /// 
        /// 
        ///         PUT
        ///         {
        ///             "nombre": "Nuevo",
        ///             "imagen": "NotImage",
        ///             "timeStams": "2021-12-20T01:56:35.181"
        ///         }
        /// 
        /// </remarks>
        /// <response code="200">Se edito el genero correctamente.</response>
        /// <response code="400">No se pudo editar el genero</response>
        /// <response code="404">El genero no existe</response>


        [HttpPut]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Put(GeneroPutViewModel generoPutViewModel)
        {
            var editarGenero = _unitOfWork.Genero.GetGenero(generoPutViewModel.Id);

            if (editarGenero == null)
            {
                return NotFound();
            }



            try
            {
                editarGenero.Nombre = generoPutViewModel.Nombre;
                editarGenero.Imagen = generoPutViewModel.Imagen;
                editarGenero.TimeStams = generoPutViewModel.TimeStams;
                if (ModelState.IsValid)
                {
                    _unitOfWork.Genero.UpdateEntity(editarGenero);
                    _unitOfWork.Complete();
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
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
           var generoDelete = _unitOfWork.Genero.GetGenero(id);
            if(generoDelete == null)
            {
                return NotFound($"el genero con ID {id} no existe");
            }
            try
            {
                _unitOfWork.Genero.DeleteEntity(id);
            }
            catch (Exception)
            {

               return BadRequest();
            }
            
            return Ok();
        }

    }
}
