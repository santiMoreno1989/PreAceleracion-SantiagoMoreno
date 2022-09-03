using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.ViewModel
{
    public class PeliculaPutViewModel
    {
        public byte[] Imagen { get; set; }
        [MaxLength(5,ErrorMessage ="Ha superado el maximo de 5 caracteres.")]
        public string Titulo { get; set; }
        [Range(1,5,ErrorMessage ="La calificacion de la pelicula va del 1 al 5")]
        public short Calificacion { get; set; }
    }
}
