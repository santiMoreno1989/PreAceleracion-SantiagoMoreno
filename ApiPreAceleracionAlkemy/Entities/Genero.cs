using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Entities
{
    public class Genero 
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100,ErrorMessage ="Ha excedido el maximo de 100 caracteres")]
        [Display(Name ="Nombre")]
        public string Nombre { get; set; }
        
        [Display(Name ="Imagen")]
        public byte[] Imagen { get; set; }
        public ICollection<Pelicula> Peliculas { get; set; }
    }
}
