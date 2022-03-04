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
        private readonly UnitOfWork _unitOfWork;
        private readonly IPersonajeRepository _personajeRepository;

        public PersonajeService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public PersonajeService(IPersonajeRepository personajeRepository)
        {
            _personajeRepository = personajeRepository;
        }

        public async Task<Personaje> Create(Personaje personaje)
        {
            var Personje = await _unitOfWork.PersonajeRepository.AddEntity(personaje);
            return Personje;
        }

        public void Delete(int id)
        {
            _unitOfWork.PersonajeRepository.DeleteEntity(id);
        }

        public async Task<Personaje> Edit(Personaje personaje)
        {
            var personje = await _unitOfWork.PersonajeRepository.UpdateEntity(personaje);
            return personaje; 
        }

        public async Task<IEnumerable<Personaje>> GetAll()
        {
            var personajes = await _unitOfWork.PersonajeRepository.GetAllEntities();
            //var personajes = await _personajeRepository.GetAllEntities();
            
            return personajes.ToList();
        }

        public async Task<Personaje> GetById(int id)
        {
            var personaje = await _unitOfWork.PersonajeRepository.GetEntity(id);
            return personaje;
        }

        public async Task<IEnumerable<Personaje>> GetCustomsPersonajes(string sortOrder, string name, short? age, int? IdMovie)
        {
            var personaje = await _personajeRepository.GetAllEntities();

            if (!string.IsNullOrEmpty(name))
            {
                personaje = personaje.Where(n => n.Nombre.ToUpper() == name.ToUpper()).ToList();
            }
            if (age != null)
            {
                personaje = personaje.Where(e => e.Edad == age).ToList();
            }
            if (IdMovie != null)
            {
                personaje = personaje.Where(p => p.Peliculas.FirstOrDefault(x => x.Id == IdMovie) != null).ToList();
            }

            switch (sortOrder)
            {
                case "desc":
                    personaje = personaje.OrderByDescending(p => p.Nombre.ToUpper());
                    break;
                case "asc":
                    personaje = personaje.OrderBy(p => p.Nombre.ToUpper());
                    break;
                case "created":
                    personaje = personaje.OrderBy(p => p.CreationDate);
                    break;
                case "deleted":
                    personaje = personaje.OrderByDescending(p => p.DeletedStamp);
                    break;
                default:
                    personaje = personaje.OrderBy(p => p.Id);
                    break;
            }
            return personaje;
        }
    }
}
