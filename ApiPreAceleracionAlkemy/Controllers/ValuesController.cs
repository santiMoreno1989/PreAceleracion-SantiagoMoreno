using ApiPreAceleracionAlkemy.Filter.PersonajesFilter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(ExceptionManagerFilter))]
    public class ValuesController : ControllerBase
    {
        public ValuesController()
        {

        }
        [HttpGet]
        public ActionResult GetValuesExceptions() {

            var file = new List<IFormFile>();
                    throw new Exception("Hola soy un error");
                    

            return Ok(file);
        }
    }
}
