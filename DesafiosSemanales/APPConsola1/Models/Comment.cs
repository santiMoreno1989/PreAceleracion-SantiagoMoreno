using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPConsola1.Models
{
    class Comment
    {
       
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Comments { get; set; }
        public Usuario Usuario { get; set; }
    }
}
