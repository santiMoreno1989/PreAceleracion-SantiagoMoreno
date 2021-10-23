using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Entities
{
    public class Pelicula : PrimaryKey
    {
        public byte[] Imagen { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public short Calificacion { get; set; }
        public ICollection<Personaje> Personajes { get; set; }
        public Genero Genero { get; set; }
    }
}
