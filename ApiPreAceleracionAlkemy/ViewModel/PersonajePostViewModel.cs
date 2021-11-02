using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.ViewModel
{
    public class PersonajePostViewModel
    {
        public byte[] Imagen { get; set; }
        public string Nombre { get; set; }
        public short Edad { get; set; }
        public int Peso { get; set; }
        public string Historia { get; set; }
    }
}
