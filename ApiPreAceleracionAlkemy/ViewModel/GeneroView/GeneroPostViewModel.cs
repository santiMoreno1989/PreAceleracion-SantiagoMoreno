using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPreAceleracionAlkemy.ViewModel.GeneroView
{

    public class GeneroPostViewModel
    {

        [Required(ErrorMessage ="Required")]
        [Column(TypeName ="VARCHAR(100)")]
        [MaxLength(100, ErrorMessage = "Ha excedido el maximo de 100 caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Imagen")]
        public byte[] Imagen { get; set; }
    }
}
