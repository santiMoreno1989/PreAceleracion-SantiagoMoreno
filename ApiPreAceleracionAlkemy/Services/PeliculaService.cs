using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.Exceptions;
using ApiPreAceleracionAlkemy.ViewModel;
using ApiPreAceleracionAlkemy.ViewModel.PeliculaView;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Services
{
    public class PeliculaService : IPeliculaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PeliculaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Pelicula> Add(PeliculaPostViewModel entity)
        {
            var peliculas = await _unitOfWork.peliculaRepository.GetAll();
            var peliculaExist = peliculas.Select(p => p.Titulo.ToUpper());

            if (peliculaExist.Contains(entity.Titulo.ToUpper()))
                throw new BadRequestException("La pelicula que intenta crear ya existe en la base de datos.");

            Pelicula pelicula = new();

            _mapper.Map(entity,pelicula);

            return await _unitOfWork.peliculaRepository.Add(pelicula);
        }

        public async Task Delete(int id)
        {
            var pelicula =await GetById(id);

            await _unitOfWork.peliculaRepository.Delete(pelicula.Id);
        }

        public async Task<IEnumerable<PeliculasGetViewModel>> GetAll()
        {
            var peliculas = await _unitOfWork.peliculaRepository.GetAll();
            if (!peliculas.Any())
                throw new NotFoundException("No se han encontrado peliculas en la base de datos.");

            var result = _mapper.Map<IEnumerable<PeliculasGetViewModel>>(peliculas);

            return result;
        }

        public async Task<Pelicula> GetById(int id)
        {
            var pelicula= await _unitOfWork.peliculaRepository.GetById(id);

            if (pelicula == null)
                throw new NotFoundException("La pelicula no existe en la base de datos.");

            return pelicula;
        }

        public async Task<Pelicula> UpdateAsync(int id,PeliculaPutViewModel entity)
        {
            var result =await GetById(id);

            _mapper.Map(entity,result);

            return await _unitOfWork.peliculaRepository.Update(result);
        }
    }
}
