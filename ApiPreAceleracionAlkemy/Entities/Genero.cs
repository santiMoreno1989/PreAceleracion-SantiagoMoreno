using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Entities
{
    public class Genero : PrimaryKey
    {
        public string Nombre { get; set; }
        public byte[] Imagen { get; set; }
        public ICollection<Pelicula> Peliculas { get; set; }
    }
}
