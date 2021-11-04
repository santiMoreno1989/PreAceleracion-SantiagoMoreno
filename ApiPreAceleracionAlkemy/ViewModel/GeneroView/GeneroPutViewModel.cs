using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.ViewModel.GeneroView
{
    public class GeneroPutViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public byte[] Imagen { get; set; }
    }
}
