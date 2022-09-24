using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Entities
{
    public class Genero 
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Imagen { get; set; }

        public DateTime? SoftDelete { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public ICollection<Pelicula> Peliculas { get; set; }
    }
}
