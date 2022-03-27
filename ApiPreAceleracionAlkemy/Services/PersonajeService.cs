using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.Interfaces;
using ApiPreAceleracionAlkemy.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Services
{

    public class PersonajeService : IPersonajeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PersonajeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Personaje> Add(Personaje entity)
        {
            return await _unitOfWork.personajeRepository.Add(entity);
        }

        public async Task Delete(int id)
        {
          await _unitOfWork.personajeRepository.Delete(id);
        }

        public async Task<IEnumerable<Personaje>> GetAll()
        {
            return await _unitOfWork.personajeRepository.GetAll();
        }

        public async Task<Personaje> GetById(int id)
        {
            return await _unitOfWork.personajeRepository.GetById(id);
        }

        public async Task<Personaje> Update(Personaje entity)
        {
            return await _unitOfWork.personajeRepository.Update(entity);
        }
    }
}
