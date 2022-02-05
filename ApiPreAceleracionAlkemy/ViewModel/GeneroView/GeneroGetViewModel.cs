using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.ViewModel.GeneroView
{
    public class GeneroGetViewModel
    {
        [Required(ErrorMessage ="Required")]
        [Column(TypeName ="VARCHAR(100)")]
        [MaxLength(100, ErrorMessage = "Ha excedido el maximo de 100 caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Imagen")]
        public byte[] Imagen { get; set; }
        public DateTime? TimeStams { get; set; }
    }
}
