using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.Services;
using ApiPreAceleracionAlkemy.ViewModel;
using ApiPreAceleracionAlkemy.ViewModel.PeliculaView;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Controllers
{

    [ApiController]
    [Route(template:"api/[controller]")]
    [Produces("application/json")]
    //[Authorize(Roles = "Admin,User")]
    public class PeliculasController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IPeliculaService _peliculaService;
        public PeliculasController(IPeliculaService peliculaService,IMapper mapper)
        {
            _peliculaService = peliculaService;
            _mapper = mapper;
        }
        /// <summary>
        /// Obtiene todas las peliculas registradas
        /// </summary>
        /// <response code="200">Se listo con exito las peliculas.</response>
        /// <response code="204">No existen peliculas.</response>
        [HttpGet]
        [Route("PeliculasList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [AllowAnonymous]
        public async  Task<ActionResult<IEnumerable<PeliculasGetViewModel>>> GetPeliculas()
        {
            var peliculas =await _peliculaService.GetAll();

            var model = _mapper.Map<IEnumerable<PeliculasGetViewModel>>(peliculas);
            if (!model.Any()) { return NoContent(); }

            return Ok(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Nombre de la pelicula</param>
        /// <param name="genre">Id Genero relacionado</param>
        /// <param name="order">Orden de las peliculas</param>
        /// <response code="200">Retorna una lista de peliculas</response>
        /// <response code="204">No hay contenido</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("movies")]
        [AllowAnonymous]

        // ** TODO : APLICAR AUTOMAPPER **//
        
        public async Task<IActionResult> Get(string name,int? genre, string order)
        {
            var peliculas = await _peliculaService.GetAll();
            string ASC = "ASC";
            string DESC = "DESC";
            if (!string.IsNullOrEmpty(name))
                {
                    peliculas = (from b in peliculas where b.Titulo == name
                                select b).ToList();
                }
            
            if(genre != null)
                {
                    peliculas = peliculas.Where(x => x.Genero.FirstOrDefault(z => z.Id == genre) != null).ToList();
                }
           
             if (order == ASC)
                {
                    peliculas = (from A in peliculas
                                 orderby A.Titulo ascending
                                 select A).ToList();
            }

            if (order == DESC)
                {
                    peliculas = (from D in peliculas orderby D.Titulo descending select D).ToList();
                }


            if (!peliculas.Any()) return NoContent();

            return Ok(peliculas);
        }
        /// <summary>
        /// Permite registrar una nueva pelicula
        /// </summary>
        /// <param name="peliculaViewModel"></param>
        /// <remarks>
        /// 
        ///     **Sample Request** :
        ///         
        ///             POST
        ///             {
        ///                 "Imagen": "Image.pnj",
        ///                 "Titulo": "Harry Potter",
        ///                 "Calificacion":"5"
        ///                 
        ///                 
        ///             }
        /// 
        /// </remarks>
        /// <response code="200">Se Creo la pelicula correctamente.</response>
        /// <response code="400">No se pudo crear la pelicula</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        // ** TODO : APLICAR AUTOMAPPER **//

        public async Task<IActionResult> Post(PeliculaPostViewModel peliculaViewModel)
        {
            var pelicula = new Pelicula
            {
                Imagen = peliculaViewModel.Imagen,
                Titulo = peliculaViewModel.Titulo,
                Calificacion = peliculaViewModel.Calificacion
            };
            try
            {
              await  _peliculaService.Add(pelicula);
            }
            catch (Exception)
            {

                return BadRequest();
            }

                
        
            return Ok(pelicula);
        }
        /// <summary>
        /// Permite editar una pelicula
        /// </summary>
        /// <remarks>
        /// 
        ///      **Sample Request** :
        ///             
        ///             PUT
        ///             {
        ///                 "Imagen": "imagenPrueba.jpg",
        ///                 "Titulo":"Comedia",
        ///                 "Calificacion":"3"
        ///             }
        /// 
        /// </remarks>
        /// <param name="peliculaViewModel"></param>
        ///<response code="200">Se edito la pelicula correctamente.</response> 
        ///<response code="400">No se pudo editar la pelicula.</response>
        ///<response code="404">No se encontro la pelicula</response>

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        // ** TODO : APLICAR AUTOMAPPER **//

        public async Task<IActionResult> Put(PeliculaPutViewModel peliculaViewModel)
        {
            var  movie =await _peliculaService.GetById(peliculaViewModel.Id);

            if(movie == null)
            {
                return NotFound("Pelicula no encontrada");
            }
            movie.Imagen = peliculaViewModel.Imagen;
            movie.Titulo = peliculaViewModel.Titulo;
            movie.Calificacion = peliculaViewModel.Calificacion;

            try
            {
               await _peliculaService.Update(movie);
            }
            catch (Exception)
            {

                return BadRequest();
            }
            

            return Ok(movie);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        ///     **Sample Request** :
        ///             
        ///             DELETE 
        ///             {
        ///                 "id":"20"
        ///             }
        /// 
        /// </remarks>
        /// <param name="id"> ID de la pelicula a eliminar</param>
        /// <reponse code="200">Se elimino correctamente la pelicula.</reponse>
        /// <response code="400">No se pudo eliminar la pelicula.</response>
        /// <response code="404">No se encontro la pelicula.</response>


        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _peliculaService.Delete(id);
            
            return Ok("Se elimino la pelicula correctamente.");
        }
    }
}
