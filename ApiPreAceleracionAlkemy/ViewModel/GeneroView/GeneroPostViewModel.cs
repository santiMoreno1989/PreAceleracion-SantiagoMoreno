using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.ViewModel.GeneroView
{
    public class GeneroPostViewModel
    {
        [Required]
        public string Nombre { get; set; }
        public byte[] Imagen { get; set; }
    }
}
