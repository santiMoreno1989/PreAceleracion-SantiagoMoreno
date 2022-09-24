using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.Exceptions;
using ApiPreAceleracionAlkemy.ViewModel;
using ApiPreAceleracionAlkemy.ViewModel.GeneroView;
using ApiPreAceleracionAlkemy.Wrappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Services
{
    public class GeneroService : IGeneroService
    {

        private readonly IUnitOfWork _unitOfWork;

        public GeneroService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Genero> Add(GeneroPostViewModel generoPostViewModel)
        {
            var generos =await _unitOfWork.generoRepository.GetAll();
            var nombreGenero = generos.Select(n => n.Nombre.ToUpper());

            if (nombreGenero.Contains(generoPostViewModel.Nombre.ToUpper()))
                throw new BadRequestException("El genero que intenta crear ya existe.");

            var entity = new Genero
            {
                Nombre = generoPostViewModel.Nombre,
                Imagen= generoPostViewModel.Imagen
            };

            return await _unitOfWork.generoRepository.Add(entity);
        }

        public async Task<DeleteViewModelResponse> Delete(int id)
        {
            var generoExist = await GetById(id);

            await _unitOfWork.generoRepository.Delete(generoExist.Id);
            return new DeleteViewModelResponse
            {
                 StatusCode=200,
                 Mensaje ="Se elimino correctamente el genero."
            };
        }

        public async Task<IEnumerable<Genero>> GetAll() 
        {
            var generos = await _unitOfWork.generoRepository.GetAll();

            if (!generos.Any())
                throw new NotFoundException("No se han encontrado generos en la base de datos.");
            
            return generos;
        } 

        public async Task<Genero> GetById(int id)
        {
            var genero= await _unitOfWork.generoRepository.GetById(id);

            if (genero == null)
                throw new NotFoundException($"No se ha encontrago el genero con ID : {id}");
            
            return genero;
        }

        public async Task<Genero> Update(int id, GeneroPutViewModel entity)
        {
            var generoExist = await GetById(id);

            //if (generoExist == null)
            //    throw new NotFoundException($"El genero con ID : {entity.Id} no existe");

            generoExist.Nombre = entity.Nombre;
            generoExist.Imagen = entity.Imagen;
            generoExist.FechaModificacion = entity.FechaModificacion;

            return await _unitOfWork.generoRepository.Update(generoExist);
        }
        public async Task<IEnumerable<GeneroGetViewModel>> GetByCondition(string nombre) 
        {
            if (string.IsNullOrWhiteSpace(nombre) || nombre.Length < 3)
                throw new BadRequestException($"Debe ingresar al menos 3 caracteres para la consulta");

            var internalGenero = _unitOfWork.generoRepository.FindByCondition(
                n => n.Nombre.ToLower().Contains(nombre.ToLower())).Select(x=>(GeneroGetViewModel) x)
                .OrderBy(n=> n.Nombre);

            if (!internalGenero.Any())
                throw new NotFoundException($"No se ha encontrado el genero : {nombre} ");
            
            return internalGenero;
        }

        public async Task<Pagination<GeneroGetViewModel>> GetGenerosAsync(int pageIndex, int pageSize,string sort)
        {
            var generos = _unitOfWork.generoRepository.GetQuery(
                include: ge=> ge.Include(pe=> pe.Peliculas));
            generos = sort switch
            {
                "NOMBRE_ASC" => generos.OrderBy(n => n.Nombre),
                "NOMBRE_DESC" => generos.OrderByDescending(n => n.Nombre),

                "FECHA_ASC" => generos.OrderBy(f=> f.FechaCreacion),
                "FECHA_DESC" => generos.OrderByDescending(f=> f.FechaCreacion),

                _ => generos.OrderBy(i=> i.Id)
            };

            
            return await generos.Select(ge => (GeneroGetViewModel)ge).PaginatedResponseAsync(pageIndex, pageSize);
        }

    }
}
