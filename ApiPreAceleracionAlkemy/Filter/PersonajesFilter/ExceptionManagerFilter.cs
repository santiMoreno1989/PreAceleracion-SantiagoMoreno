using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ApiPreAceleracionAlkemy.Filter.PersonajesFilter
{
    public class ExceptionManagerFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IModelMetadataProvider _provider;

        public ExceptionManagerFilter(IWebHostEnvironment environment, IModelMetadataProvider provider)
        {
            _environment = environment;
            _provider = provider;
        }

        public void OnException(ExceptionContext context)
        {
            context.Result = new JsonResult("Fallo algo en la API" + _environment.ApplicationName+"La excepcion del tipo :"+context.Exception.GetType());
        }
    }
}
