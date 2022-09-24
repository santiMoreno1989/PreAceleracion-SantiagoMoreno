using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Entities
{
    public class Pelicula 
    {
        public int Id { get; set; }
        public string Imagen { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? SoftDelete { get; set; }
        public short Calificacion { get; set; }
        public ICollection<Personaje> Personajes { get; set; }
        public ICollection<Genero> Genero { get; set; }
    }
}