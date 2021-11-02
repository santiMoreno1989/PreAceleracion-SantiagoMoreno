using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.ViewModel
{
    public class PeliculaPutViewModel
    {
        public int Id { get; set; }
        public byte[] Imagen { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public short Calificacion { get; set; }
    }
}
