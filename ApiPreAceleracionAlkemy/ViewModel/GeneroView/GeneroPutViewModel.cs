using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.ViewModel.GeneroView
{
    public class GeneroPutViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(90,ErrorMessage ="Ha excedido el numero maximo de 90 caracteres.")]
        public string Nombre { get; set; }
        public byte[] Imagen { get; set; }

    }
}
