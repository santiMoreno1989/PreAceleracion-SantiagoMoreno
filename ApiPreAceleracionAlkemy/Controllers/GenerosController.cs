using ApiPreAceleracionAlkemy.Data;
using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.Repositories;
using ApiPreAceleracionAlkemy.ViewModel.GeneroView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Controllers
{
    [ApiController]
    [Route(template:"api/[controller]")]
    [Authorize(Roles = "Admin,User")]
    public class GenerosController : ControllerBase
    {

       
        private readonly IUnitOfWork _unitOfWork;
        public GenerosController(IUnitOfWork unitOfWork)
        {
           
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Prueba")]
        public   IActionResult TryGet()
        {
            var modelGeneros = _unitOfWork.Genero.GetAllEntities();
            var userVM = new List<GeneroGetViewModel>();
            foreach (var item in modelGeneros)
            {
                userVM.Add(
                    new GeneroGetViewModel
                    {
                        Nombre = item.Nombre,
                        Imagen = item.Imagen
                    }
                    );
            }
            return Ok(userVM);
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

        [HttpPost]
        public IActionResult Post(GeneroPostViewModel generoPostViewModel)
        {
            var genero = new Genero
            {
                Nombre = generoPostViewModel.Nombre,
                Imagen = generoPostViewModel.Imagen
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
        [HttpPut]
        [AllowAnonymous]
        public IActionResult Put(GeneroPutViewModel generoPutViewModel)
        {
            var editarGenero = _unitOfWork.Genero.GetGenero(generoPutViewModel.Id);

            if (editarGenero == null)
            {
                return NotFound(new
                {
                    status = "Error",
                    Messege = $"El Genero {generoPutViewModel.Nombre} no existe en la base de datos."
                });
            }



            try
            {
                editarGenero.Nombre = generoPutViewModel.Nombre;
                editarGenero.Imagen = generoPutViewModel.Imagen;
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
                    return BadRequest(new
                    {
                        status = "Error",
                        Messege = $"El Genero {generoPutViewModel.Nombre} no se pudo editar correctamente."
                    });
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
            
            return Ok($"El genero {generoDelete.Nombre} ha sido eliminado correctamente.");
        }

    }
}
