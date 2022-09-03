using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.ViewModel
{
    public class PeliculaPostViewModel
    {
        [Display(Name ="Imagen")]
        public byte[] Imagen { get; set; }

        [MaxLength(50, ErrorMessage ="Ha superado el maximo de 50 caracteres.")]
        [Display(Name ="Titulo")]
        [Required(ErrorMessage ="Este campo es requerido.")]
        public string Titulo { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        
        [Display(Name ="Calificacion")]
        [Range(1,5,ErrorMessage ="ERROR : La calificacion debe estar entre 1 y 5.")]
        public short Calificacion { get; set; }
    }
}
