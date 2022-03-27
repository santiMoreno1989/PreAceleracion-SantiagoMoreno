using ApiPreAceleracionAlkemy.Filter.PersonajesFilter;
using ApiPreAceleracionAlkemy.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace ApiPreAceleracionAlkemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly PositionOptions _positionOptions;
        public ValuesController(IOptions<PositionOptions> options )
        {
            _positionOptions = options.Value;
        }
        [HttpGet]
        public ActionResult GetValues() {

            var title = _positionOptions.Name;
            var name = _positionOptions.Title;
            return Ok(new
            {
                titulo = title,
                nombre = name

            }); 
        } 
    }
}
