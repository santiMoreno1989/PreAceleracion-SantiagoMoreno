using ApiPreAceleracionAlkemy.Data;
using ApiPreAceleracionAlkemy.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Controllers
{
    public class GenerosController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public GenerosController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {

            return Ok(_context.Generos.ToList());
        }
        [HttpPost]
        public IActionResult Post(Genero genero)
        {
            _context.Generos.Add(genero);
            _context.SaveChanges();
            return Ok(_context.Generos.ToList());
        }
        [HttpPut]
        public IActionResult Put()
        {
            return Ok();
        }
        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok();
        }

    }
}
