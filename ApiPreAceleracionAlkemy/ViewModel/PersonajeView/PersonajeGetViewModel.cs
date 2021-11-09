using ApiPreAceleracionAlkemy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.ViewModel.PersonajeView
{
    public class PersonajeGetViewModel
    {
        public int Id { get; set; }
        public byte[] Imagen { get; set; }
        public string name { get; set; }

    }
}
