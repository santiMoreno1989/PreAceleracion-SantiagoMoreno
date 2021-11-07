using ApiPreAceleracionAlkemy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.ViewModel.PersonajeView
{
    public class PersonajeGetViewModel
    {
        public string name { get; set; }
        public short age { get; set; }
        public List<int> movies { get; set; } = new List<int>();
    }
}
