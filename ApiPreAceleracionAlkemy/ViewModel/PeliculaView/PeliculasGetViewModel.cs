using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.ViewModel.PeliculaView
{
    public class PeliculasGetViewModel
    {
        public byte[] Imagen { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
