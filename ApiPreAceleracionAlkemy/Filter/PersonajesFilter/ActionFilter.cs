using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace ApiPreAceleracionAlkemy.Filter.PersonajesFilter
{
    public class ActionFilter : ActionFilterAttribute
    {
        private readonly ILogger<ActionFilter> _logger;

        public ActionFilter(ILogger<ActionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogTrace("antes del metodo");
            base.OnActionExecuted(context);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogTrace("Despues del metodo");
            base.OnActionExecuting(context);
        }
    }
}
