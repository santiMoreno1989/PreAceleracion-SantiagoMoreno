using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.ViewModel.GeneroView
{
    /// <summary>
    /// Permite registrar los generos de las peliculas
    /// </summary>
    public class GeneroPostViewModel
    {
        /// <summary>
        /// Obtiene o establece el nombre del genero
        /// </summary>
        [Required(ErrorMessage ="El campo nombre es obligatorio.")]
        [MaxLength(100, ErrorMessage = "Ha excedido el maximo de 100 caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        /// <summary>
        /// Obtiene o establece la imagen del genero
        /// </summary>
        [Display(Name = "Imagen")]
        public byte[] Imagen { get; set; }
        /// <summary>
        /// obtiene o establece la fecha de eliminacion del genero
        /// </summary>
        public DateTime? TimeStams { get; set; }
    }
}
