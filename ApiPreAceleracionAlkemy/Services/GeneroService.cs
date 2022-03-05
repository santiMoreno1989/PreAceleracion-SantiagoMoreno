using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.Interfaces;
using ApiPreAceleracionAlkemy.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Services
{
    public class GeneroService : IGeneroService 
    {
        private readonly IGeneroRepository _generoRepository;

        public GeneroService(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        public async Task<Genero> Create(Genero genero)
        {
            var genro =await _generoRepository.AddEntity(genero);
            return genro;
        }

        public void Delete(int id)
        {
            _generoRepository.DeleteEntity(id);
        }

        public async Task<Genero> Edit(Genero genero)
        {
            var gnero =await _generoRepository.UpdateEntity(genero);
            return gnero;
        }

        public async Task<IEnumerable<Genero>> GetAll()
        {
            var generos =await _generoRepository.GetAllEntities();
            return generos;
        }

        public async Task<Genero> GetById(int id)
        {
            var genero =await _generoRepository.GetEntity(id);
            return genero;
        }
    }
}
