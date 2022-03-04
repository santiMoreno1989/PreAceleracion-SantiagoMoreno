using ApiPreAceleracionAlkemy.Entities;
using ApiPreAceleracionAlkemy.Interfaces;
using ApiPreAceleracionAlkemy.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Services
{
    //TODO : Implementar logica de negocios que esta aplicada en el controlador de Peliculas //
    public class PeliculaService : IPeliculaService
    {
        private readonly IPeliculaRepository _peliculaRepository;

        public PeliculaService(IPeliculaRepository peliculaRepository)
        {
            _peliculaRepository = peliculaRepository;
        }

        public async Task<Pelicula> Create(Pelicula peli)
        {
            var pelicula = await _peliculaRepository.AddEntity(peli);
            return pelicula;
        }

        public void Delete(int id)
        {
            var pelicula = _peliculaRepository.DeleteEntity(id);
        }

        public async Task<Pelicula> Edit(Pelicula peli)
        {
            var pelicula = await _peliculaRepository.UpdateEntity(peli);
            return pelicula;
        }

        public async Task<IEnumerable<Pelicula>> GetAll()
        {
            var peliculas =await _peliculaRepository.GetAllEntities();
            return peliculas.ToList();
        }

        public async Task<Pelicula> GetById(int id)
        {
            var pelicula =await _peliculaRepository.GetEntity(id);
            return pelicula;
        }
    }
}
