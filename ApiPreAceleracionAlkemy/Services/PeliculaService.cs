using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.Interfaces;
using ApiPreAceleracionAlkemy.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Services
{
    public class PeliculaService : IPeliculaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PeliculaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Pelicula> Add(Pelicula entity)
        {
            return await _unitOfWork.peliculaRepository.Add(entity);
        }

        public async Task  Delete(int id)
        {
              await _unitOfWork.peliculaRepository.Delete(id);
        }

        public async Task<IEnumerable<Pelicula>> GetAll()
        {
            return await _unitOfWork.peliculaRepository.GetAll();
        }

        public async Task<Pelicula> GetById(int id)
        {
            return await _unitOfWork.peliculaRepository.GetById(id);
        }

        public async Task<Pelicula> Update(Pelicula entity)
        {
            return await _unitOfWork.peliculaRepository.Update(entity);
        }
    }
}
